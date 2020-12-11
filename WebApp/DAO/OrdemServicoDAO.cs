﻿using System;
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
    /// Ordens de Serviço
    /// </summary>
    public class OrdemServicoDAO : Conexao
    {
        // *************** OrdemServico  *************************************************************

        /// <summary>
        ///     Lista de todas as OSs não deletadas
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
            try
            {
                List<OrdemServico> lst = new List<OrdemServico>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORDENS_SERVICO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@filtroOrdemServico_codigo", filtroOrdemServico_codigo != null ?  filtroOrdemServico_codigo.Trim() : null);
                    com.Parameters.AddWithValue("@filtroObj_codigo", filtroObj_codigo != null ? filtroObj_codigo.Trim() : null );

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
                        lst.Add(new OrdemServico
                        {
                            row_numero = Convert.ToDouble(rdr["row_numero"]),
                            row_expandida = Convert.ToDouble(rdr["row_expandida"]),
                            nNivel = Convert.ToDouble(rdr["nNivel"]),
                            temFilhos = Convert.ToInt32(rdr["temFilhos"]),

                            ord_id = Convert.ToInt32(rdr["ord_id"]),
                            ord_codigo = rdr["ord_codigo"].ToString(),
                            ord_descricao = rdr["ord_descricao"].ToString(),
                            
                            ord_pai = rdr["ord_pai"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ord_pai"]),
                            ocl_id = rdr["ocl_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ocl_id"]),
                            tos_id = rdr["tos_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["tos_id"]),
                            sos_id = rdr["sos_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["sos_id"]),

                            obj_id = rdr["obj_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["obj_id"]),
                            ord_ativo = rdr["ord_ativo"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ord_ativo"]),
                            ord_criticidade = rdr["ord_criticidade"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ord_criticidade"]),

                            tos_codigo = rdr["tos_codigo"] == DBNull.Value ? string.Empty : rdr["tos_codigo"].ToString(),
                            tos_descricao = rdr["tos_descricao"] == DBNull.Value ? string.Empty : rdr["tos_descricao"].ToString(),
                            ocl_codigo = rdr["ocl_codigo"] == DBNull.Value ? string.Empty : rdr["ocl_codigo"].ToString(),
                            ocl_descricao = rdr["ocl_descricao"] == DBNull.Value ? string.Empty : rdr["ocl_descricao"].ToString(),
                            sos_codigo = rdr["sos_codigo"] == DBNull.Value ? string.Empty : rdr["sos_codigo"].ToString(),
                            sos_descricao = rdr["sos_descricao"] == DBNull.Value ? string.Empty : rdr["sos_descricao"].ToString(),
                            obj_codigo = rdr["obj_codigo"] == DBNull.Value ? string.Empty : rdr["obj_codigo"].ToString(),
                            obj_descricao = rdr["obj_descricao"] == DBNull.Value ? string.Empty : rdr["obj_descricao"].ToString(),

                            ord_codigo_pai = rdr["ord_codigo_pai"] == DBNull.Value ? string.Empty : rdr["ord_codigo_pai"].ToString(),
                            ord_descricao_pai = rdr["ord_descricao_pai"] == DBNull.Value ? string.Empty : rdr["ord_descricao_pai"].ToString(),
                            tpu_codigo_der = rdr["tpu_codigo_der"] == DBNull.Value ? string.Empty : rdr["tpu_codigo_der"].ToString(),
                            tpu_descricao = rdr["tpu_descricao"] == DBNull.Value ? string.Empty : rdr["tpu_descricao"].ToString(),

                            con_codigofiscalizacao = rdr["con_codigofiscalizacao"] == DBNull.Value ? string.Empty : rdr["con_codigofiscalizacao"].ToString(),
                            con_descricaofiscalizacao = rdr["con_descricaofiscalizacao"] == DBNull.Value ? string.Empty : rdr["con_descricaofiscalizacao"].ToString(),
                            con_codigoexecucao = rdr["con_codigoexecucao"] == DBNull.Value ? string.Empty : rdr["con_codigoexecucao"].ToString(),
                            con_descricaoexecucao = rdr["con_descricaoexecucao"] == DBNull.Value ? string.Empty : rdr["con_descricaoexecucao"].ToString(),

                            con_codigoorcamento = rdr["con_codigoorcamento"] == DBNull.Value ? string.Empty : rdr["con_codigoorcamento"].ToString(),
                            con_descricaoorcamento = rdr["con_descricaoorcamento"] == DBNull.Value ? string.Empty : rdr["con_descricaoorcamento"].ToString(),
                            ord_aberta_por_usuario = rdr["ord_aberta_por_usuario"] == DBNull.Value ? string.Empty : rdr["ord_aberta_por_usuario"].ToString(),
                            ord_aberta_por_nome = rdr["ord_aberta_por_nome"] == DBNull.Value ? string.Empty : rdr["ord_aberta_por_nome"].ToString(),

                            con_id = rdr["con_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["con_id"]),
                            ord_data_inicio_programada = rdr["ord_data_inicio_programada"] == DBNull.Value ? string.Empty : rdr["ord_data_inicio_programada"].ToString(),
                            ord_data_termino_programada = rdr["ord_data_termino_programada"] == DBNull.Value ? string.Empty : rdr["ord_data_termino_programada"].ToString(),
                            ord_data_inicio_execucao = rdr["ord_data_inicio_execucao"] == DBNull.Value ? string.Empty : rdr["ord_data_inicio_execucao"].ToString(),
                            ord_data_termino_execucao = rdr["ord_data_termino_execucao"] == DBNull.Value ? string.Empty : rdr["ord_data_termino_execucao"].ToString(),
                            ord_quantidade_estimada = rdr["ord_quantidade_estimada"] == DBNull.Value ? -1 : Convert.ToDouble(rdr["ord_quantidade_estimada"]),
                            uni_id_qt_estimada = rdr["uni_id_qt_estimada"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["uni_id_qt_estimada"]),
                            ord_quantidade_executada = rdr["ord_quantidade_executada"] == DBNull.Value ? -1 : Convert.ToDouble(rdr["ord_quantidade_executada"]),
                            uni_id_qt_executada = rdr["uni_id_qt_executada"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["uni_id_qt_executada"]),
                            ord_custo_estimado = rdr["ord_custo_estimado"] == DBNull.Value ? -1 : Convert.ToDouble(rdr["ord_custo_estimado"]),
                            ord_custo_final = rdr["ord_custo_final"] == DBNull.Value ? -1 : Convert.ToDouble(rdr["ord_custo_final"]),
                            ord_aberta_por = rdr["ord_aberta_por"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ord_aberta_por"]),
                            ord_data_abertura = rdr["ord_data_abertura"] == DBNull.Value ? string.Empty : rdr["ord_data_abertura"].ToString(),
                            ord_responsavel_der = rdr["ord_responsavel_der"] == DBNull.Value ? string.Empty : rdr["ord_responsavel_der"].ToString(),
                            ord_responsavel_fiscalizacao = rdr["ord_responsavel_fiscalizacao"] == DBNull.Value ? string.Empty : rdr["ord_responsavel_fiscalizacao"].ToString(),
                            con_id_fiscalizacao = rdr["con_id_fiscalizacao"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["con_id_fiscalizacao"]),
                            ord_responsavel_execucao = rdr["ord_responsavel_execucao"] == DBNull.Value ? string.Empty : rdr["ord_responsavel_execucao"].ToString(),
                            con_id_execucao = rdr["con_id_execucao"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["con_id_execucao"]),
                            ord_responsavel_suspensao = rdr["ord_responsavel_suspensao"] == DBNull.Value ? string.Empty : rdr["ord_responsavel_suspensao"].ToString(),
                            ord_data_suspensao = rdr["ord_data_suspensao"] == DBNull.Value ? string.Empty : rdr["ord_data_suspensao"].ToString(),
                            ord_responsavel_cancelamento = rdr["ord_responsavel_cancelamento"] == DBNull.Value ? string.Empty : rdr["ord_responsavel_cancelamento"].ToString(),
                            ord_data_cancelamento = rdr["ord_data_cancelamento"] == DBNull.Value ? string.Empty : rdr["ord_data_cancelamento"].ToString(),
                            ord_data_reinicio = rdr["ord_data_reinicio"] == DBNull.Value ? string.Empty : rdr["ord_data_reinicio"].ToString(),
                            con_id_orcamento = rdr["con_id_orcamento"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["con_id_orcamento"]),
                            tpt_id = rdr["tpt_id"] == DBNull.Value ? string.Empty : rdr["tpt_id"].ToString(),
                            tpu_data_base_der = rdr["tpu_data_base_der"] == DBNull.Value ? string.Empty : rdr["tpu_data_base_der"].ToString(),
                            tpu_id = rdr["tpu_id"] == DBNull.Value ? string.Empty : rdr["tpu_id"].ToString(),
                            tpu_preco_unitario = rdr["tpu_preco_unitario"] == DBNull.Value ? -1 : Convert.ToDouble(rdr["tpu_preco_unitario"]),
                            lst_proximos_status = rdr["lst_proximos_status"] == DBNull.Value ? string.Empty : rdr["lst_proximos_status"].ToString()

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
        ///    Altera os dados da Ordem de Servico no Banco
        /// </summary>
        /// <param name="ord">Nome da Ordem de Servico</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrdemServico_Salvar(OrdemServico ord, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_UPD_ORDEM_SERVICO";
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (ord.ord_id > 0)
                        com.Parameters.AddWithValue("@ord_id", ord.ord_id);

                    com.Parameters.AddWithValue("@ord_codigo", ord.ord_codigo);
                    com.Parameters.AddWithValue("@ord_descricao", ord.ord_descricao);
                    com.Parameters.AddWithValue("@ord_pai", ord.ord_pai);
                    com.Parameters.AddWithValue("@ocl_id", ord.ocl_id);
                    com.Parameters.AddWithValue("@tos_id", ord.tos_id);
                    com.Parameters.AddWithValue("@sos_id", ord.sos_id);
                    com.Parameters.AddWithValue("@obj_id", ord.obj_id);
                    com.Parameters.AddWithValue("@ord_ativo", ord.ord_ativo);

                    if (ord.ord_criticidade > 0)
                        com.Parameters.AddWithValue("@ord_criticidade", ord.ord_criticidade);

                    if (ord.con_id > 0)
                        com.Parameters.AddWithValue("@con_id", ord.con_id);

                    com.Parameters.AddWithValue("@ord_data_inicio_programada", ord.ord_data_inicio_programada);
                    com.Parameters.AddWithValue("@ord_data_termino_programada", ord.ord_data_termino_programada);

                    com.Parameters.AddWithValue("@ord_data_inicio_execucao", ord.ord_data_inicio_execucao);
                    com.Parameters.AddWithValue("@ord_data_termino_execucao", ord.ord_data_termino_execucao);

                    if (ord.ord_quantidade_estimada > 0)
                        com.Parameters.AddWithValue("@ord_quantidade_estimada", ord.ord_quantidade_estimada);

                    com.Parameters.AddWithValue("@uni_id_qt_estimada", ord.uni_id_qt_estimada);

                    if (ord.ord_quantidade_executada > 0)
                        com.Parameters.AddWithValue("@ord_quantidade_executada", ord.ord_quantidade_executada);

                    com.Parameters.AddWithValue("@uni_id_qt_executada", ord.uni_id_qt_executada);

                    if (ord.ord_custo_estimado > 0)
                        com.Parameters.AddWithValue("@ord_custo_estimado", ord.ord_custo_estimado);

                    if (ord.ord_custo_final > 0)
                        com.Parameters.AddWithValue("@ord_custo_final", ord.ord_custo_final);

                    //com.Parameters.AddWithValue("@ord_aberta_por", ord.ord_aberta_por);
                    //com.Parameters.AddWithValue("@ord_data_abertura", ord.ord_data_abertura);
                    com.Parameters.AddWithValue("@ord_responsavel_der", ord.ord_responsavel_der);
                    com.Parameters.AddWithValue("@ord_responsavel_fiscalizacao", ord.ord_responsavel_fiscalizacao);

                    com.Parameters.AddWithValue("@con_id_fiscalizacao", ord.con_id_fiscalizacao);
                    com.Parameters.AddWithValue("@ord_responsavel_execucao", ord.ord_responsavel_execucao);
                    com.Parameters.AddWithValue("@con_id_execucao", ord.con_id_execucao);

                    com.Parameters.AddWithValue("@ord_responsavel_suspensao", ord.ord_responsavel_suspensao);
                    com.Parameters.AddWithValue("@ord_data_suspensao", ord.ord_data_suspensao);

                    com.Parameters.AddWithValue("@ord_responsavel_cancelamento", ord.ord_responsavel_cancelamento);
                    com.Parameters.AddWithValue("@ord_data_cancelamento", ord.ord_data_cancelamento);

                    com.Parameters.AddWithValue("@ord_data_reinicio", ord.ord_data_reinicio);

                    com.Parameters.AddWithValue("@con_id_orcamento", ord.con_id_orcamento);
                    com.Parameters.AddWithValue("@tpt_id", ord.tpt_id);
                    com.Parameters.AddWithValue("@tpu_data_base_der", ord.tpu_data_base_der);
                    com.Parameters.AddWithValue("@tpu_id", ord.tpu_id);
                    com.Parameters.AddWithValue("@tpu_preco_unitario", ord.tpu_preco_unitario);
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
        ///    Insere os dados da Ordem de Servico no Banco
        /// </summary>
        /// <param name="ord">Ordem de Servico</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrdemServico_Inserir_Novo(OrdemServico ord, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_INS_ORDEM_SERVICO_NOVA";
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@ord_codigo", ord.ord_codigo);
                    com.Parameters.AddWithValue("@ord_descricao", ord.ord_descricao);
                    com.Parameters.AddWithValue("@tos_id", ord.tos_id);
                    com.Parameters.AddWithValue("@obj_id", ord.obj_id);
                    com.Parameters.AddWithValue("@ord_ativo", ord.ord_ativo);
                    com.Parameters.AddWithValue("@ord_data_inicio_programada", ord.ord_data_inicio_programada);
                    com.Parameters.AddWithValue("@ord_aberta_por", ord.ord_aberta_por);
                    com.Parameters.AddWithValue("@ord_data_abertura", ord.ord_data_abertura);
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
        ///   Busca o proximo sequencial do codigo para o tipo especificado
        /// </summary>
        /// <param name="tos_id">Id do tipo selecionado</param>
        /// <returns>int</returns>
        public string OrdemServico_ProximoCodigo(int tos_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_SEL_ORDEM_SERVICO_PROXIMO_CODIGO";
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@tos_id", tos_id);

                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        return reader[0].ToString() ;
                    }
                    return "";
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
        ///     Excluir (logicamente) Ordem de Servico
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Serviço Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrdemServico_Excluir(int ord_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ORDEM_SERVICO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ord_id", ord_id);
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
        ///  Ativa/Desativa Ordem de Servico
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Servico Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrdemServico_AtivarDesativar(int ord_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ORDEM_SERVICO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ord_id", ord_id);
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
        ///    Associa Documentos à Ordem de Serviço selecionada
        /// </summary>
        /// <param name="doc_ids">Ids dos Documentos selecionados</param>
        /// <param name="ord_id">Id da OrdemServico selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrdemServico_AssociarDocumentos(string doc_ids, int ord_id, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ORDEMSERVICO_ASSOCIAR_DOCUMENTOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@doc_ids", doc_ids);
                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@usu_id_logado", usu_id);
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
        ///    Associa Documentos de Referência à Ordem de Serviço selecionada
        /// </summary>
        /// <param name="doc_id">Ids do Documentos selecionado</param>
        /// <param name="ord_id">Id da OrdemServico selecionada</param>
        /// <param name="dos_referencia">Zero para associar; Um para desassociar</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrdemServico_AssociarDocumentosReferencia(int doc_id, int ord_id, int dos_referencia, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ORDEMSERVICO_ASSOCIAR_DOCUMENTOS_REFERENCIA", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@doc_id", doc_id);
                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@dos_referencia", dos_referencia);
                    com.Parameters.AddWithValue("@usu_id_logado", usu_id);
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
        ///    Desassocia Documento da Ordem de Serviço selecionada
        /// </summary>
        /// <param name="doc_id">Id do Documento selecionado</param>
        /// <param name="ord_id">Id da OrdemServico selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrdemServico_DesassociarDocumento(int doc_id, int ord_id, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ORDEMSERVICO_DESASSOCIAR_DOCUMENTO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@doc_id", doc_id);
                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@usu_id_logado", usu_id);
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
        /// Lista de todos os Documentos Associados à Ordem de Serviço selecionada
        /// </summary>
        /// <param name="ord_id">Id da OrdemServico selecionada</param>
        /// <param name="obj_id">Id do Objeto da OrdemServico selecionada</param>
        /// <param name="somente_referencia">Retornar somente os documentos de referência</param>
        /// <returns>Lista de Documentos</returns>
        public List<Documento> OrdemServico_Documentos_ListAll(int ord_id, int obj_id, int? somente_referencia = 0)
        {
            try
            {
                string CaminhoVirtualRaizArquivos = new ParametroDAO().Parametro_GetValor("CaminhoVirtualRaizArquivos");
                List<Documento> lst = new List<Documento>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORDEMSERVICO_DOCUMENTOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@somente_referencia", somente_referencia);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        string doc_caminho = rdr["doc_caminho"].ToString();
                        if (doc_caminho.Trim() != "")
                        {
                            if (new Gerais().RemoteFileExists(CaminhoVirtualRaizArquivos + doc_caminho))
                                doc_caminho = CaminhoVirtualRaizArquivos + doc_caminho;
                            else
                                doc_caminho = "";
                        }

                        lst.Add(new Documento
                        {
                            doc_id = Convert.ToInt32(rdr["doc_id"]),
                            doc_codigo = rdr["doc_codigo"].ToString(),
                            doc_descricao = rdr["doc_descricao"].ToString(),
                            tpd_id = rdr["tpd_id"].ToString(),
                            doc_caminho = doc_caminho,
                            doc_ativo = Convert.ToInt16(rdr["doc_ativo"]),
                            tpd_descricao = rdr["tpd_descricao"].ToString(),
                            dos_referencia = Convert.ToInt16(rdr["dos_referencia"]),
                            total_registros = Convert.ToInt32(rdr["total_registros"])
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
        /// Lista de todos os Documentos Associados ao Objeto da Ordem de Servico selecionada
        /// </summary>
        /// <param name="ord_id">Id do OrdemServico selecionado</param>
        /// <returns>Lista de Documentos</returns>
        public List<Documento> OrdemServico_Objeto_Documentos_ListAll(int ord_id)
        {
            try
            {
                string CaminhoVirtualRaizArquivos = new ParametroDAO().Parametro_GetValor("CaminhoVirtualRaizArquivos");
                List<Documento> lst = new List<Documento>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETO_DOCUMENTOS2", con);
                    com.CommandType = CommandType.StoredProcedure;
                    //com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@ord_id", ord_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        string doc_caminho = rdr["doc_caminho"].ToString();
                        if (doc_caminho.Trim() != "")
                        {
                            if (new Gerais().RemoteFileExists(CaminhoVirtualRaizArquivos + doc_caminho))
                                doc_caminho = CaminhoVirtualRaizArquivos + doc_caminho;
                            else
                                doc_caminho = "";
                        }

                        lst.Add(new Documento
                        {
                            doc_id = Convert.ToInt32(rdr["doc_id"]),
                            doc_codigo = rdr["doc_codigo"].ToString(),
                            doc_descricao = rdr["doc_descricao"].ToString(),
                            tpd_id = rdr["tpd_id"].ToString(),
                            doc_caminho = doc_caminho,
                            doc_ativo = Convert.ToInt16(rdr["doc_ativo"]),
                            tpd_descricao = rdr["tpd_descricao"].ToString(),
                            dos_referencia = Convert.ToInt16(rdr["dos_referencia"])
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
        ///  Lista de todos os Documentos não associados para a Ordem de Serviço selecionada
        /// </summary>
        /// <param name="ord_id">Id da OrdemServico selecionada</param>
        /// <param name="codDoc">Codigo ou parte do Documento a procurar</param>
        /// <returns>Lista de Documentos Nao Associados</returns>
        public List<Documento> OrdemServico_DocumentosNaoAssociados_ListAll(int ord_id, string codDoc)
        {
            try
            {
                List<Documento> lst = new List<Documento>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORDEMSERVICO_DOCUMENTOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@doc_codigo", codDoc);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Documento
                        {
                            doc_id = Convert.ToInt32(rdr["doc_id"]),
                            doc_codigo = rdr["doc_codigo"].ToString(),
                            doc_descricao = rdr["doc_descricao"].ToString(),
                            tpd_id = rdr["tpd_id"].ToString(),
                            doc_caminho = rdr["doc_caminho"].ToString(),
                            doc_ativo = Convert.ToInt16(rdr["doc_ativo"]),
                            tpd_descricao = rdr["tpd_descricao"].ToString(),
                            total_registros = Convert.ToInt32(rdr["total_registros"])
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







        // *************** Classe Ordem Servico  *************************************************************

        /// <summary>
        ///     Lista CLASSES de OS não deletadas / null para todos
        /// </summary>
        /// <param name="ocl_id">Id da Classe de Ordem de Serviço Selecionado</param>
        /// <returns>Lista de OSClasse</returns>
        public List<OSClasse> OSClasse_ListAll(int? ocl_id = null)
        {
            try
            {
                List<OSClasse> lst = new List<OSClasse>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OS_CLASSES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@ocl_id", ocl_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new OSClasse
                        {
                            ocl_id = Convert.ToInt32(rdr["ocl_id"]),
                            ocl_codigo = rdr["ocl_codigo"].ToString(),
                            ocl_descricao = rdr["ocl_descricao"].ToString(),
                            ocl_ativo = Convert.ToInt16(rdr["ocl_ativo"])
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
        ///    Insere ou Altera os dados da Classe da Ordem de Servico no Banco
        /// </summary>
        /// <param name="tos">Dados da Classe de Ordem de Servico</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSClasse_Salvar(OSClasse tos, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (tos.ocl_id > 0)
                        com.CommandText = "STP_UPD_OS_CLASSE";
                    else
                        com.CommandText = "STP_INS_OS_CLASSE";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (tos.ocl_id > 0)
                        com.Parameters.AddWithValue("@ocl_id", tos.ocl_id);

                    com.Parameters.AddWithValue("@ocl_codigo", tos.ocl_codigo.ToUpper());
                    com.Parameters.AddWithValue("@ocl_descricao", tos.ocl_descricao);
                    com.Parameters.AddWithValue("@ocl_ativo", tos.ocl_ativo);
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
        ///     Excluir (logicamente) Classe de Ordem de Servico
        /// </summary>
        /// <param name="ocl_id">Id da Classe de Ordem de Serviço Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSClasse_Excluir(int ocl_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_OS_CLASSE", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocl_id", ocl_id);
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
        ///  Ativa/Desativa Ordem de Servico
        /// </summary>
        /// <param name="ocl_id">Id da Classe de Ordem de Servico Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSClasse_AtivarDesativar(int ocl_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_OS_CLASSE", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocl_id", ocl_id);
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


        // *************** Tipo Ordem Servico  *************************************************************

        /// <summary>
        ///     Lista Tipos de OS não deletados / null para todos
        /// </summary>
        /// <param name="tos_id">Id do Tipo de Ordem de Serviço Selecionado</param>
        /// <returns>Lista de OSTipo</returns>
        public List<OSTipo> OSTipo_ListAll(int? tos_id = null)
        {
            try
            {
                List<OSTipo> lst = new List<OSTipo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OS_TIPOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@tos_id", tos_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new OSTipo
                        {
                            tos_id = Convert.ToInt32(rdr["tos_id"]),
                            tos_codigo = rdr["tos_codigo"].ToString(),
                            tos_descricao = rdr["tos_descricao"].ToString(),
                            tos_ativo = Convert.ToInt16(rdr["tos_ativo"])
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
        ///    Insere ou Altera os dados do Tipo da Ordem de Servico no Banco
        /// </summary>
        /// <param name="tos">Dados do Tipo de Ordem de Servico</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSTipo_Salvar(OSTipo tos, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (tos.tos_id > 0)
                        com.CommandText = "STP_UPD_OS_TIPO";
                    else
                        com.CommandText = "STP_INS_OS_TIPO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (tos.tos_id > 0)
                        com.Parameters.AddWithValue("@tos_id", tos.tos_id);

                    com.Parameters.AddWithValue("@tos_codigo", tos.tos_codigo.ToUpper());
                    com.Parameters.AddWithValue("@tos_descricao", tos.tos_descricao);
                    com.Parameters.AddWithValue("@tos_ativo", tos.tos_ativo);
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
        ///     Excluir (logicamente) Tipo de Ordem de Servico
        /// </summary>
        /// <param name="tos_id">Id do Tipo de Ordem de Serviço Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSTipo_Excluir(int tos_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_OS_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@tos_id", tos_id);
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
        ///  Ativa/Desativa Ordem de Servico
        /// </summary>
        /// <param name="tos_id">Id do Tipo de Ordem de Servico Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSTipo_AtivarDesativar(int tos_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_OS_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@tos_id", tos_id);
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


        // *************** Status Ordem Servico  *************************************************************

        /// <summary>
        ///     Lista de Status de OS não deletados / null para todos
        /// </summary>
        /// <param name="sos_id">Id do Status de Ordem de Servico / vazio para todos </param>
        /// <returns>Lista de OrdemServico</returns>
        public List<OSStatus> OSStatus_ListAll(int? sos_id = null)
        {
            try
            {
                List<OSStatus> lst = new List<OSStatus>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OS_STATUS", con);

                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    if (sos_id != null)
                        com.Parameters.AddWithValue("@sos_id", sos_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new OSStatus
                        {
                            sos_id = Convert.ToInt16(rdr["sos_id"]),
                            sos_codigo = rdr["sos_codigo"].ToString(),
                            sos_descricao = rdr["sos_descricao"].ToString(),
                            sos_ativo = Convert.ToInt16(rdr["sos_ativo"])
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
        ///    Insere ou Altera os dados do Status de Ordem de Servico
        /// </summary>
        /// <param name="sos">Dados do Status de Ordem de Servico</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSStatus_Salvar(OSStatus sos, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    if (sos.sos_id > 0)
                        com.CommandText = "STP_UPD_OS_STATUS";
                    else
                        com.CommandText = "STP_INS_OS_STATUS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (sos.sos_id > 0)
                        com.Parameters.AddWithValue("@sos_id", sos.sos_id);

                    com.Parameters.AddWithValue("@sos_codigo", sos.sos_codigo.ToUpper());
                    com.Parameters.AddWithValue("@sos_descricao", sos.sos_descricao);
                    com.Parameters.AddWithValue("@sos_ativo", sos.sos_ativo);
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
        ///     Excluir (logicamente) Status de Ordem de Servico
        /// </summary>
        /// <param name="sos_id">Id do Status de Ordem de Serviço Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSStatus_Excluir(int sos_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_OS_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@sos_id", sos_id);
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
        ///  Ativa/Desativa Status de Ordem de Servico
        /// </summary>
        /// <param name="sos_id">Id do Status de Ordem de Servico Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSStatus_AtivarDesativar(int sos_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_OS_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@sos_id", sos_id);
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



        // *************** Fluxo de Status de Ordem Servico  *************************************************************

        /// <summary>
        ///     Lista de Fluxo de Status de OS não deletados / null para todos
        /// </summary>
        /// <param name="fos_id">Id do Fluxo de Status de Ordem de Servico / vazio para todos </param>
        /// <returns>Lista de OrdemServico</returns>
        public List<OSFluxoStatus> OSFluxoStatus_ListAll(int? fos_id = null)
        {
            try
            {
                List<OSFluxoStatus> lst = new List<OSFluxoStatus>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OS_FLUXOSTATUS", con);

                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    if (fos_id != null)
                        com.Parameters.AddWithValue("@fos_id", fos_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new OSFluxoStatus
                        {
                            fos_id = Convert.ToInt16(rdr["fos_id"]),
                            fos_descricao = rdr["fos_descricao"].ToString(),
                            fos_ativo = Convert.ToInt16(rdr["fos_ativo"]),

                            sos_id_de = Convert.ToInt16(rdr["sos_id_de"]),
                            sos_de_codigo = rdr["sos_de_codigo"].ToString(),
                            sos_de_descricao = rdr["sos_de_descricao"].ToString(),

                            sos_id_para = Convert.ToInt16(rdr["sos_id_para"]),
                            sos_para_codigo = rdr["sos_para_codigo"].ToString(),
                            sos_para_descricao = rdr["sos_para_descricao"].ToString()
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
        ///    Insere ou Altera os dados do Fluxo de Status de Ordem de Servico
        /// </summary>
        /// <param name="fos">Dados do Fluxo de Status de Ordem de Servico</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSFluxoStatus_Salvar(OSFluxoStatus fos, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    if (fos.fos_id > 0)
                        com.CommandText = "STP_UPD_OS_FLUXOSTATUS";
                    else
                        com.CommandText = "STP_INS_OS_FLUXOSTATUS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (fos.fos_id > 0)
                        com.Parameters.AddWithValue("@fos_id", fos.fos_id);

                    com.Parameters.AddWithValue("@sos_id_de", fos.sos_id_de);
                    com.Parameters.AddWithValue("@sos_id_para", fos.sos_id_para);
                    com.Parameters.AddWithValue("@fos_descricao", fos.fos_descricao);
                    com.Parameters.AddWithValue("@fos_ativo", fos.fos_ativo);
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
        ///     Excluir (logicamente) Fluxo de Status de Ordem de Servico
        /// </summary>
        /// <param name="fos_id">Id do Fluxo de Status de Ordem de Serviço Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSFluxoStatus_Excluir(int fos_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_OS_FLUXOSTATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@fos_id", fos_id);
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
        ///  Ativa/Desativa Status de Ordem de Servico
        /// </summary>
        /// <param name="fos_id">Id do Fluxo de Status de Ordem de Servico Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OSFluxoStatus_AtivarDesativar(int fos_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_OS_FLUXOSTATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@fos_id", fos_id);
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