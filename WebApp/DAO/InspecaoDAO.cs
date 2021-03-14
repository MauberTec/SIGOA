using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebApp.Models;
using WebApp.Helpers;
using System.Diagnostics;

namespace WebApp.DAO
{
    /// <summary>
    /// ANOMALIAS
    /// </summary>
    public class InspecaoDAO : Conexao
    {


        // *************** INSPECAO  *************************************************************

        /// <summary>
        ///     Lista de todas os Inspeções não deletadas
        /// </summary>
        /// <param name="ins_id">Filtro por Id da Inspeção, null para todos</param>
        /// <param name="filtroOrdemServico_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtroObj_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtroTiposOS">Id do Tipo a se filtrar</param>
        /// <param name="filtroStatusOS">Id do Status a se filtrar</param>
        /// <param name="filtroData">Filtro pelo tipo de Data Selecionado</param>
        /// <param name="filtroord_data_De">Filtro por Data: a de</param>
        /// <param name="filtroord_data_Ate">Filtro por Data: até</param>
        /// <returns>Lista de Inspecao</returns>
        public List<Inspecao> Inspecao_ListAll(int? ins_id, string filtroOrdemServico_codigo = null, string filtroObj_codigo = null, int? filtroTiposOS = -1, int? filtroStatusOS = -1, string filtroData = "", string filtroord_data_De = "", string filtroord_data_Ate = "")
        {
            try
            {
                List<Inspecao> lst = new List<Inspecao>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_INSPECOES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ins_id", ins_id);

                    com.Parameters.AddWithValue("@filtroOrdemServico_codigo", filtroOrdemServico_codigo != null ? filtroOrdemServico_codigo.Trim() : null);
                    com.Parameters.AddWithValue("@filtroObj_codigo", filtroObj_codigo != null ? filtroObj_codigo.Trim() : null);

                    if (filtroTiposOS >= 0)
                        com.Parameters.AddWithValue("@filtroTiposOS", filtroTiposOS);

                    if (filtroStatusOS >= 0)
                        com.Parameters.AddWithValue("@filtroStatusOS", filtroStatusOS);

                    if (filtroData != "")
                    {
                        com.Parameters.AddWithValue("@filtroData", filtroData);

                        if (filtroord_data_De != "")
                            com.Parameters.AddWithValue("@filtroord_data_De", filtroord_data_De);

                        if (filtroord_data_Ate != "")
                            com.Parameters.AddWithValue("@filtroord_data_Ate", filtroord_data_Ate);
                    }

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Inspecao
                        {
                            ins_id = Convert.ToInt32(rdr["ins_id"]),
                            ord_id = Convert.ToInt32(rdr["ord_id"]),
                            ord_codigo = rdr["ord_codigo"].ToString(),
                            ord_descricao = rdr["ord_descricao"].ToString(),
                            tos_id = Convert.ToInt32(rdr["tos_id"]),
                            tos_codigo = rdr["tos_codigo"].ToString(),
                            tos_descricao = rdr["tos_descricao"].ToString(),
                            sos_id = Convert.ToInt32(rdr["sos_id"]),
                            sos_codigo = rdr["sos_codigo"].ToString(),
                            sos_descricao = rdr["sos_descricao"].ToString(),
                            obj_id = Convert.ToInt32(rdr["obj_id"]),
                            obj_codigo = rdr["obj_codigo"].ToString(),
                            obj_descricao = rdr["obj_descricao"].ToString(),
                            ins_ativo = Convert.ToInt16(rdr["ins_ativo"])
                            //, registro_ini = Convert.ToInt32(rdr["registro_ini"]),
                            //total_registros = Convert.ToInt32(rdr["total_registros"])
                        });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Carrega o grid somente com a página solicitada
        /// </summary>
        /// <param name="ord_id">Filtro por Id de O.S., null para todos</param>
        /// <param name="ord_codigo">Filtro por Código de O.S., vazio para todos</param>
        /// <param name="ord_descricao">Filtro por Descrição de O.S., vazio para todos</param>
        /// <param name="ipt_id">Filtro por Tipo de O.S., vazio para todos</param>
        /// <param name="dcl_codigo">Filtro por Classe de Projeto de O.S., vazio para todos</param>
        /// <param name="start">Número do registro inícial da página</param>
        /// <param name="length">Quantidade de registros por página</param>
        /// <param name="Order_BY">Ordenado por</param>
        /// <returns>List do tipo O.S.</returns>
        public List<Inspecao> Inspecao_ListAll(int? ord_id, string ord_codigo = "", string ord_descricao = "", string ipt_id = "", string dcl_codigo = "", int start = 0, int length = 10, string Order_BY = "")
        {
            try
            {
                List<Inspecao> lstInspecao = new List<Inspecao>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_DOCUMENTOS_2", con);
                    com.CommandType = CommandType.StoredProcedure;

                    com.CommandTimeout = 600; // (tempo em segundos)
                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@ord_codigo", ord_codigo);
                    com.Parameters.AddWithValue("@ord_descricao", ord_descricao);
                    com.Parameters.AddWithValue("@ipt_id", ipt_id);
                    com.Parameters.AddWithValue("@dcl_codigo", dcl_codigo);

                    com.Parameters.AddWithValue("@registro_ini", start);
                    com.Parameters.AddWithValue("@ordenado_por", Order_BY);
                    com.Parameters.AddWithValue("@qt_por_pagina", length);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lstInspecao.Add(new Inspecao
                        {
                            ord_id = Convert.ToInt32(rdr["ord_id"]),
                            ord_codigo = rdr["ord_codigo"].ToString(),
                            ord_descricao = rdr["ord_descricao"].ToString(),
                            //ipt_id = rdr["ipt_id"].ToString(),
                            //dcl_codigo = (rdr["dcl_codigo"] == DBNull.Value) ? "" : rdr["dcl_codigo"].ToString(),
                            //dcl_descricao = (rdr["dcl_descricao"] == DBNull.Value) ? "" : rdr["dcl_descricao"].ToString(),

                            //ord_ativo = Convert.ToInt16(rdr["ord_ativo"]),
                            //ipt_descricao = rdr["ipt_descricao"].ToString(),
                            //dcl_id = (rdr["dcl_id"] == DBNull.Value) ? 0 : Convert.ToInt16(rdr["dcl_id"]),
                            registro_ini = Convert.ToInt32(rdr["registro_ini"]),
                            total_registros = Convert.ToInt32(rdr["total_registros"])
                        });
                    }
                    return lstInspecao;
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista os Atributos do Objeto da O.S.selecionada, para o preenchimento de ficha de inspecao
        /// </summary>
        /// <param name="ord_id">Id da O.S.selecionada</param>
        /// <returns>Lista de ObjAtributoValores</returns>
        public List<ObjAtributoValores> InspecaoAtributosValores_ListAll(int ord_id)
        {
            try
            {
                List<ObjAtributoValores> lst = new List<ObjAtributoValores>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_INSPECAO_ATRIBUTOS_VALORES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandTimeout = 600; // (tempo em segundos)
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@ord_id", ord_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new ObjAtributoValores
                        {
                            obj_id = (rdr["obj_id"] == DBNull.Value) ? -1 : Convert.ToInt32(rdr["obj_id"]),
                            atr_id = Convert.ToInt32(rdr["atr_id"]),
                            nItens = Convert.ToInt32(rdr["nItens"]),

                            atr_atributo_nome = rdr["atr_atributo_nome"].ToString(),
                            atr_descricao = rdr["atr_descricao"].ToString(),
                            atr_mascara_texto = (rdr["atr_mascara_texto"] == DBNull.Value) ? string.Empty : rdr["atr_mascara_texto"].ToString(),
                            atv_controle = rdr["atv_controle"].ToString(),

                            ati_ids = (rdr["ati_ids"] == DBNull.Value) ? "" : rdr["ati_ids"].ToString(),
                            atv_valor = (rdr["atv_valor"] == DBNull.Value) ? string.Empty : rdr["atv_valor"].ToString(),
                            atv_valores = (rdr["atv_valores"] == DBNull.Value) ? string.Empty : rdr["atv_valores"].ToString(),
                            atr_apresentacao_itens = (rdr["atr_apresentacao_itens"] == DBNull.Value) ? string.Empty : rdr["atr_apresentacao_itens"].ToString(),
                            // ati_item = (rdr["ati_item"] == DBNull.Value) ? string.Empty : rdr["ati_item"].ToString(),
                            atr_itens_todos = rdr["atr_itens_todos"].ToString()

                            //obj_id = (rdr["obj_id"] == DBNull.Value) ? -1 : Convert.ToInt32(rdr["obj_id"]),
                            //atr_id = Convert.ToInt32(rdr["atr_id"]),
                            //clo_id = Convert.ToInt32(rdr["clo_id"]),
                            //tip_id = Convert.ToInt32(rdr["tip_id"]),
                            //nItens = Convert.ToInt32(rdr["nItens"]),

                            //atr_atributo_nome = rdr["atr_atributo_nome"].ToString(),
                            //atr_descricao = rdr["atr_descricao"].ToString(),
                            //atr_mascara_texto = rdr["atr_mascara_texto"].ToString(),
                            //atv_controle = rdr["atv_controle"].ToString(),

                            //ati_ids = (rdr["ati_ids"] == DBNull.Value) ? "" : rdr["ati_ids"].ToString(),
                            //atv_valor = (rdr["atv_valor"] == DBNull.Value) ? string.Empty : rdr["atv_valor"].ToString(),
                            //atv_valores = (rdr["atv_valores"] == DBNull.Value) ? string.Empty : rdr["atv_valores"].ToString(),
                            //atr_apresentacao_itens = (rdr["atr_apresentacao_itens"] == DBNull.Value) ? string.Empty : rdr["atr_apresentacao_itens"].ToString(),
                            //// ati_item = (rdr["ati_item"] == DBNull.Value) ? string.Empty : rdr["ati_item"].ToString(),
                            //atr_itens_todos = rdr["atr_itens_todos"].ToString()
                        });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        ///  Salva os Valores dos Atributos  no Banco
        /// </summary>
        /// <param name="ObjAtributoValor">Valor do Atributo</param>
        /// <param name="codigoOAE">Código da Obra de Arte</param>
        /// <param name="selidTipoOAE">Id do Tipo de Obra de Arte</param>
        /// <param name="ord_id">Id da Ordem de Serviço</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int InspecaoAtributoValores_Salvar(ObjAtributoValores ObjAtributoValor, string codigoOAE, int selidTipoOAE, int ord_id, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "STP_UPD_INSPECAO_ATRIBUTO_VALORES";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;
                    com.Parameters.AddWithValue("@obj_id", ObjAtributoValor.obj_id);

                    if (ObjAtributoValor.atr_id != -1)
                        com.Parameters.AddWithValue("@atr_id", ObjAtributoValor.atr_id);

                    if (ObjAtributoValor.ati_id != "-1")
                        com.Parameters.AddWithValue("@ati_id", ObjAtributoValor.ati_id);

                    com.Parameters.AddWithValue("@nome_aba", ObjAtributoValor.nome_aba);
                    com.Parameters.AddWithValue("@atv_valores", ObjAtributoValor.atv_valores);

                    com.Parameters.AddWithValue("@codigoOAE", codigoOAE);
                    com.Parameters.AddWithValue("@selidTipoOAE", selidTipoOAE);

                    if (ord_id > 0)
                        com.Parameters.AddWithValue("@ord_id", ord_id);

                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    com.ExecuteScalar();
                    return Convert.ToInt32(p_return.Value);
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }



        //********** INSPECAO ANOMALIAS *********************************************************************************************

        /// <summary>
        /// Lista das anomalias encontradas no Objeto da O.S.selecionada, para o preenchimento de ficha de inspecao
        /// </summary>
        /// <param name="ord_id">Id da O.S.selecionada</param>
        /// <returns>Lista de InspecaoAnomalia</returns>
        public List<InspecaoAnomalia> InspecaoAnomalias_Valores_ListAll(int ord_id)
        {
            try
            {
                int obj_id_anterior = -1;
                int obj_id_atual = -1;


                List<InspecaoAnomalia> lst = new List<InspecaoAnomalia>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_INSPECAO_ANOMALIAS_VALORES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandTimeout = 600; // (tempo em segundos)
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@ord_id", ord_id);

                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                       obj_id_atual = Convert.ToInt32(rdr["obj_id"]);

                       lst.Add(new InspecaoAnomalia
                        {
                           //ins_id = Convert.ToInt32(rdr["ins_id"]),

                           obj_codigo_TipoOAE = rdr["obj_codigo_TipoOAE"].ToString(),
                           ins_anom_Responsavel = rdr["ins_anom_Responsavel"].ToString(),
                           ins_anom_data = rdr["ins_anom_data"].ToString(),
                           ins_anom_quadroA_1 = rdr["ins_anom_quadroA_1"].ToString(),
                           ins_anom_quadroA_2 = rdr["ins_anom_quadroA_2"].ToString(),

                            rownum = Convert.ToInt32(rdr["rownum"]),
                            obj_id = obj_id_atual,
                            obj_pai = Convert.ToInt32(rdr["obj_pai"]),
                            obj_codigo = rdr["obj_codigo"].ToString(),
                            obj_descricao = obj_id_atual != obj_id_anterior ? rdr["obj_descricao"].ToString() : "",

                            level = Convert.ToInt32(rdr["level"]),
                            item = rdr["item"].ToString(),
                            //path = rdr["path"].ToString(),
                            clo_id = Convert.ToInt32(rdr["clo_id"]),
                            clo_nome = rdr["clo_nome"].ToString(),
                            tip_id = Convert.ToInt32(rdr["tip_id"]),
                            tip_nome = rdr["tip_nome"].ToString() ,

                            col_Localizacao =  rdr["col_Localizacao"] == DBNull.Value ? "" : rdr["col_Localizacao"].ToString(),
                           
                            ian_id = rdr["ian_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ian_id"]),

                            ian_numero = ((rdr["ian_numero"] == DBNull.Value) ||  (Convert.ToInt32(rdr["ian_numero"]) == 0)) ? "" : rdr["ian_numero"].ToString(),

                            atp_id = rdr["atp_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["atp_id"]),
                            atp_codigo = rdr["atp_codigo"] == DBNull.Value ? "" : rdr["atp_codigo"].ToString(),
                            atp_descricao = rdr["atp_descricao"] == DBNull.Value ? "" : rdr["atp_descricao"].ToString(),

                            ale_id = rdr["ale_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ale_id"]),
                            ale_codigo = rdr["ale_codigo"] == DBNull.Value ? "" : rdr["ale_codigo"].ToString(),
                            ale_descricao = rdr["ale_descricao"] == DBNull.Value ? "" : rdr["ale_descricao"].ToString(),

                            aca_id = rdr["aca_id"] == DBNull.Value ? 1 : Convert.ToInt32(rdr["aca_id"]),
                            aca_codigo = rdr["aca_codigo"] == DBNull.Value ? "" : rdr["aca_codigo"].ToString(),
                            aca_descricao = rdr["aca_descricao"] == DBNull.Value ? "" : rdr["aca_descricao"].ToString(),

                            leg_id = rdr["leg_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["leg_id"]),
                            leg_codigo = rdr["leg_codigo"] == DBNull.Value ? "" : rdr["leg_codigo"].ToString(),
                            leg_descricao = rdr["leg_descricao"] == DBNull.Value ? "" : rdr["leg_descricao"].ToString(),

                            ian_sigla = rdr["ian_sigla"] == DBNull.Value ? "" : rdr["ian_sigla"].ToString(),

                           ian_quantidade = rdr["ian_quantidade"] == DBNull.Value ? "" : rdr["ian_quantidade"].ToString(),
                           ian_espacamento = rdr["ian_espacamento"] == DBNull.Value ? "" : rdr["ian_espacamento"].ToString(),
                           ian_largura = rdr["ian_largura"] == DBNull.Value ? "" : rdr["ian_largura"].ToString(),
                           ian_comprimento = rdr["ian_comprimento"] == DBNull.Value ? "" : rdr["ian_comprimento"].ToString(),
                           ian_abertura_minima = rdr["ian_abertura_minima"] == DBNull.Value ? "" : rdr["ian_abertura_minima"].ToString(),
                           ian_abertura_maxima = rdr["ian_abertura_maxima"] == DBNull.Value ? "" : rdr["ian_abertura_maxima"].ToString(),

                           ian_fotografia = rdr["ian_fotografia"] == DBNull.Value ? "" : rdr["ian_fotografia"].ToString(),
                            ian_croqui = rdr["ian_croqui"] == DBNull.Value ? "" : rdr["ian_croqui"].ToString(),
                            ian_desenho = rdr["ian_desenho"] == DBNull.Value ? "" : rdr["ian_desenho"].ToString(),
                            ian_observacoes = rdr["ian_observacoes"] == DBNull.Value ? "" : rdr["ian_observacoes"].ToString(),

                            ian_ativo = rdr["ian_ativo"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ian_ativo"]),

                            rpt_id_sugerido = rdr["rpt_id_sugerido"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["rpt_id_sugerido"]),
                            rpt_id_sugerido_codigo = rdr["rpt_id_sugerido_codigo"] == DBNull.Value ? "" : rdr["rpt_id_sugerido_codigo"].ToString(),
                            rpt_id_sugerido_descricao = rdr["rpt_id_sugerido_descricao"] == DBNull.Value ? "" : rdr["rpt_id_sugerido_descricao"].ToString(),
                            rpt_id_sugerido_unidade = rdr["rpt_id_sugerido_unidade"] == DBNull.Value ? "" : rdr["rpt_id_sugerido_unidade"].ToString(),
                            ian_quantidade_sugerida = rdr["ian_quantidade_sugerida"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["ian_quantidade_sugerida"]),

                            rpt_id_adotado = rdr["rpt_id_adotado"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["rpt_id_adotado"]),
                            rpt_id_adotado_codigo = rdr["rpt_id_adotado_codigo"] == DBNull.Value ? "" : rdr["rpt_id_adotado_codigo"].ToString(),
                            rpt_id_adotado_descricao = rdr["rpt_id_adotado_descricao"] == DBNull.Value ? "" : rdr["rpt_id_adotado_descricao"].ToString(),
                            rpt_id_adotado_unidade = rdr["rpt_id_adotado_unidade"] == DBNull.Value ? "" : rdr["rpt_id_adotado_unidade"].ToString(),
                            ian_quantidade_adotada = rdr["ian_quantidade_adotada"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["ian_quantidade_adotada"]),

                           apt_id = rdr["apt_id"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["apt_id"]),
                           apt_descricao = rdr["apt_descricao"] == DBNull.Value ? "" : rdr["apt_descricao"].ToString(),

                            lstLegendas = rdr["lstLegendas"] == DBNull.Value ? "" : rdr["lstLegendas"].ToString(),
                            lstAlertas = rdr["lstAlertas"] == DBNull.Value ? "" : rdr["lstAlertas"].ToString(),
                            lstTipos = rdr["lstTipos"] == DBNull.Value ? "" : rdr["lstTipos"].ToString(),
                            lstCausas = rdr["lstCausas"] == DBNull.Value ? "" : rdr["lstCausas"].ToString(),
                            lstReparoTipos = rdr["lstReparoTipos"] == DBNull.Value ? "" : rdr["lstReparoTipos"].ToString()

                        });

                        obj_id_anterior = Convert.ToInt32(rdr["obj_id"]);
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

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
        /// <param name="usu_id">Id do usuario logado</param>
        /// <param name="ip">Ip do usuario logado</param>
        /// <returns>int</returns>
        public int InspecaoAnomalias_Valores_Salvar(int ord_id, string ins_anom_Responsavel, string ins_anom_data, string ins_anom_quadroA_1, string ins_anom_quadroA_2, string listaConcatenada, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    SqlCommand com = new SqlCommand();
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "STP_UPD_INSPECAO_ANOMALIAS_VALORES";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@ins_anom_Responsavel", ins_anom_Responsavel);
                    com.Parameters.AddWithValue("@ins_anom_data", ins_anom_data);
                    com.Parameters.AddWithValue("@ins_anom_quadroA_1", ins_anom_quadroA_1);
                    com.Parameters.AddWithValue("@ins_anom_quadroA_2", ins_anom_quadroA_2);
                    com.Parameters.AddWithValue("@listaConcatenada", listaConcatenada);

                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);


                    com.ExecuteScalar();
                    return Convert.ToInt32(p_return.Value);
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        ///    Insere Anomalia na posicao da tabela
        /// </summary>
        /// <param name="ian_id">Id da linha da tabela inspecao_anomalias a ser inserida</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int InspecaoAnomalia_Nova(int ian_id, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_INS_INSPECOES_ANOMALIA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@ian_id", ian_id);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    com.ExecuteScalar();
                    return Convert.ToInt32(p_return.Value);
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///    Exclui logicamente Anomalia
        /// </summary>
        /// <param name="ian_id">Id da linha da tabela inspecao_anomalias</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int InspecaoAnomalia_Excluir(int ian_id, int usu_id, string ip)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "STP_DEL_INSPECAO_ANOMALIA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    com.Parameters.AddWithValue("@ian_id", ian_id);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);


                    i = com.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }




        /// <summary>
        ///    Insere Objetos a serem inspecionados
        /// </summary>
        /// <param name="ord_id">Id da O.S. dessa inspeção</param>
        /// <param name="obj_ids">Lista dos Ids dos Objetos a serem inspecionados</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int InspecaoAnomaliaObjetos_Salvar(int ord_id, string obj_ids, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "STP_INS_INSPECAO_ANOMALIA_OBJETOS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@obj_ids", obj_ids);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    com.ExecuteScalar();
                    return Convert.ToInt32(p_return.Value);
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// lista concatenada dos tipos de anomalia por legenda
        /// </summary>
        /// <param name="leg_codigo">Filtro por Legenda de Anomalia, opcional</param>
        /// <returns>string</returns>
        public string InspecaoAnomaliaTipos_by_Legenda(string leg_codigo)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    SqlCommand com = new SqlCommand("SELECT dbo.ConcatenarAnomaliaTipos_by_Legenda(@leg_codigo)", con);
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@leg_codigo", leg_codigo);

                    string retorno =  com.ExecuteScalar().ToString();

                    return retorno; 
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// lista concatenada das causas de anomalia por legenda
        /// </summary>
        /// <param name="leg_codigo">Filtro por Legenda de Anomalia, opcional</param>
        /// <returns>string</returns>
        public string InspecaoAnomaliaCausas_by_Legenda(string leg_codigo)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    SqlCommand com = new SqlCommand("SELECT dbo.ConcatenarAnomaliaCausas_by_Legenda(@leg_codigo)", con);
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@leg_codigo", leg_codigo);

                    string retorno =  com.ExecuteScalar().ToString();

                    return retorno; 
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// lista concatenada dos Alertas de anomalia por legenda
        /// </summary>
        /// <param name="leg_codigo">Filtro por Legenda de Anomalia, opcional</param>
        /// <returns>string</returns>
        public string InspecaoAnomaliaAlertas_by_Legenda(string leg_codigo)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    SqlCommand com = new SqlCommand("SELECT dbo.ConcatenarAnomaliaAlertas_by_Legenda(@leg_codigo)", con);
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@leg_codigo", leg_codigo);

                    string retorno =  com.ExecuteScalar().ToString();

                    return retorno; 
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

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
            try
            {
                List<ReparoTipo> lst = new List<ReparoTipo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_INSPECAO_ANOMALIAS_REPARO_SUGERIDO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandTimeout = 600; // (tempo em segundos)
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@leg_codigo", leg_codigo);
                    com.Parameters.AddWithValue("@atp_codigo", atp_codigo);
                    com.Parameters.AddWithValue("@ale_codigo", ale_codigo);
                    com.Parameters.AddWithValue("@aca_codigo", aca_codigo);
                    com.Parameters.AddWithValue("@rpt_area", rpt_area);

                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new ReparoTipo
                        {
                            rpt_id = rdr["rpt_id"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["rpt_id"]),
                            rpt_codigo = rdr["rpt_codigo"] == DBNull.Value ? "" : rdr["rpt_codigo"].ToString(),
                            rpt_descricao = rdr["rpt_descricao"] == DBNull.Value ? "" : rdr["rpt_descricao"].ToString(),
                            rpt_unidade = rdr["rpt_unidade"] == DBNull.Value ? "" : rdr["rpt_unidade"].ToString().Trim()
                        });

                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Lista das anomalias encontradas no Objeto da O.S.selecionada, para o preenchimento de ficha de inspecao
        /// </summary>
        /// <param name="ord_id">Id da O.S.selecionada</param>
        /// <returns>Lista de InspecaoAnomalia</returns>
        public List<InspecaoAnomalia> InspecaoAnomalias_Valores_Providencias_ListAll(int ord_id)
        {
            try
            {
                int apt_id_anterior = 0;
                List<InspecaoAnomalia> lst = new List<InspecaoAnomalia>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_INSPECAO_ANOMALIAS_VALORES_PROVIDENCIAS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandTimeout = 600; // (tempo em segundos)
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@ord_id", ord_id);

                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (apt_id_anterior != (rdr["apt_id"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["apt_id"])))
                        {
                            lst.Add(new InspecaoAnomalia
                            {
                                ehCabecalho = "1",
                                apt_id = rdr["apt_id"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["apt_id"]),
                                apt_descricao = rdr["apt_descricao"] == DBNull.Value ? "" : rdr["apt_descricao"].ToString()
                            });
                        }

                        lst.Add(new InspecaoAnomalia
                        {
                            ehCabecalho = "",
                            obj_codigo_TipoOAE = rdr["obj_codigo_TipoOAE"].ToString(),
                            ins_anom_Responsavel = rdr["ins_anom_Responsavel"].ToString(),
                            ins_anom_data = rdr["ins_anom_data"].ToString(),

                            col_Localizacao = rdr["col_Localizacao"].ToString(),
                            clo_id = Convert.ToInt32(rdr["clo_id"]),
                            clo_nome = rdr["clo_nome"].ToString(),
                            tip_id = Convert.ToInt32(rdr["tip_id"]),
                            tip_nome = rdr["tip_nome"].ToString(),
                            obj_id = Convert.ToInt32(rdr["obj_id"]),
                            obj_codigo = rdr["obj_codigo"].ToString(),
                            obj_descricao = rdr["obj_descricao"].ToString(),

                            ian_id = rdr["ian_id"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ian_id"]),
                            ian_numero = rdr["ian_numero"].ToString(),

                            atp_id = rdr["atp_id"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["atp_id"]),
                            atp_codigo = rdr["atp_codigo"] == DBNull.Value ? "" : rdr["atp_codigo"].ToString(),
                            atp_descricao = rdr["atp_descricao"] == DBNull.Value ? "" : rdr["atp_descricao"].ToString(),

                            ale_id = rdr["ale_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ale_id"]),
                            ale_codigo = rdr["ale_codigo"] == DBNull.Value ? "" : rdr["ale_codigo"].ToString(),
                            ale_descricao = rdr["ale_descricao"] == DBNull.Value ? "" : rdr["ale_descricao"].ToString(),

                            aca_id = rdr["aca_id"] == DBNull.Value ? 1 : Convert.ToInt32(rdr["aca_id"]),
                            aca_codigo = rdr["aca_codigo"] == DBNull.Value ? "" : rdr["aca_codigo"].ToString(),
                            aca_descricao = rdr["aca_descricao"] == DBNull.Value ? "" : rdr["aca_descricao"].ToString(),

                            leg_id = rdr["leg_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["leg_id"]),
                            leg_codigo = rdr["leg_codigo"] == DBNull.Value ? "" : rdr["leg_codigo"].ToString(),
                            leg_descricao = rdr["leg_descricao"] == DBNull.Value ? "" : rdr["leg_descricao"].ToString(),

                            ian_sigla = rdr["ian_sigla"] == DBNull.Value ? "" : rdr["ian_sigla"].ToString(),

                            ian_quantidade = rdr["ian_quantidade"] == DBNull.Value ? "" : rdr["ian_quantidade"].ToString(),
                            ian_espacamento = rdr["ian_espacamento"] == DBNull.Value ? "" : rdr["ian_espacamento"].ToString(),
                            ian_largura = rdr["ian_largura"] == DBNull.Value ? "" : rdr["ian_largura"].ToString(),
                            ian_comprimento = rdr["ian_comprimento"] == DBNull.Value ? "" : rdr["ian_comprimento"].ToString(),
                            ian_abertura_minima = rdr["ian_abertura_minima"] == DBNull.Value ? "" : rdr["ian_abertura_minima"].ToString(),
                            ian_abertura_maxima = rdr["ian_abertura_maxima"] == DBNull.Value ? "" : rdr["ian_abertura_maxima"].ToString(),


                            //ian_quantidade = rdr["ian_quantidade"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ian_quantidade"]),
                            //ian_espacamento = rdr["ian_espacamento"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ian_espacamento"]),
                            //ian_largura = rdr["ian_largura"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ian_largura"]),
                            //ian_comprimento = rdr["ian_comprimento"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ian_comprimento"]),
                            //ian_abertura_minima = rdr["ian_abertura_minima"] == DBNull.Value ? 0 : Math.Round(Convert.ToDouble(rdr["ian_abertura_minima"]), 1),
                            //ian_abertura_maxima = rdr["ian_abertura_maxima"] == DBNull.Value ? 0 : Math.Round(Convert.ToDouble(rdr["ian_abertura_maxima"]), 1),

                            ian_fotografia = rdr["ian_fotografia"] == DBNull.Value ? "" : rdr["ian_fotografia"].ToString(),
                            ian_croqui = rdr["ian_croqui"] == DBNull.Value ? "" : rdr["ian_croqui"].ToString(),
                            ian_desenho = rdr["ian_desenho"] == DBNull.Value ? "" : rdr["ian_desenho"].ToString(),
                            ian_observacoes = rdr["ian_observacoes"] == DBNull.Value ? "" : rdr["ian_observacoes"].ToString(),

                            ian_ativo = rdr["ian_ativo"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ian_ativo"]),
                            apt_id = rdr["apt_id"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["apt_id"]),
                            apt_descricao = rdr["apt_descricao"] == DBNull.Value ? "" : rdr["apt_descricao"].ToString(),

                            lstLegendas = rdr["lstLegendas"] == DBNull.Value ? "" : rdr["lstLegendas"].ToString(),
                            lstAlertas = rdr["lstAlertas"] == DBNull.Value ? "" : rdr["lstAlertas"].ToString(),
                            lstTipos = rdr["lstTipos"] == DBNull.Value ? "" : rdr["lstTipos"].ToString(),
                            lstCausas = rdr["lstCausas"] == DBNull.Value ? "" : rdr["lstCausas"].ToString()

                        });

                        apt_id_anterior = rdr["apt_id"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["apt_id"]);
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

        }




        // *********** tipos de inspecao ********************** *******************************

        /// <summary>
        ///     Lista de todos os Tipos de Inspecao não deletados
        /// </summary>
        /// <param name="ipt_id">Filtro por Id do Tipo de Inspecao, null para todos</param>
        /// <returns>Lista de Tipo de Inspecao</returns>
        public List<InspecaoTipo> InspecaoTipo_ListAll(int? ipt_id)
        {
            try
            {
                List<InspecaoTipo> lst = new List<InspecaoTipo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_INSPECAO_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ipt_id", ipt_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new InspecaoTipo
                        {
                            ipt_id = Convert.ToInt32(rdr["ipt_id"]),
                            ipt_codigo = rdr["ipt_codigo"].ToString(),
                            ipt_descricao = rdr["ipt_descricao"].ToString(),
                            ipt_ativo = Convert.ToInt16(rdr["ipt_ativo"])
                        });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///    Insere ou Altera os dados do Tipo de Inspecao no Banco
        /// </summary>
        /// <param name="ipt">Tipo de Inspecao</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int InspecaoTipo_Salvar(InspecaoTipo ipt, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (ipt.ipt_id > 0)
                        com.CommandText = "STP_UPD_INSPECAO_TIPO";
                    else
                        com.CommandText = "STP_INS_INSPECAO_TIPO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (ipt.ipt_id > 0)
                        com.Parameters.AddWithValue("@ipt_id", ipt.ipt_id);

                    com.Parameters.AddWithValue("@ipt_codigo", ipt.ipt_codigo);
                    com.Parameters.AddWithValue("@ipt_descricao", ipt.ipt_descricao);
                    com.Parameters.AddWithValue("@ipt_ativo", ipt.ipt_ativo);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    com.ExecuteScalar();
                    return Convert.ToInt32(p_return.Value);
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///     Excluir (logicamente) Tipo de Inspecao
        /// </summary>
        /// <param name="ipt_id">Id do Tipo de Inspecao Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int InspecaoTipo_Excluir(int ipt_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_INSPECAO_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ipt_id", ipt_id);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    i = com.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Ativa/Desativa Tipo de Inspecao
        /// </summary>
        /// <param name="ipt_id">Id do Tipo de Inspecao Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int InspecaoTipo_AtivarDesativar(int ipt_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_INSPECAO_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ipt_id", ipt_id);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    i = com.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }





    }

}