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
using System.Globalization;

namespace WebApp.DAO
{
    /// <summary>
    /// OrcamentoS
    /// </summary>
    public class OrcamentoDAO : Conexao
    {
        CultureInfo culturePTBR = new CultureInfo("pt-BR");

        /// <summary>
        ///     Lista de todos os Orcamentos não deletados
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="filtroRodovia">Filtro por Rodovia</param>
        /// <param name="filtroObjetos">Filtro por Objeto</param>
        /// <param name="filtroStatus">Filtro por Status</param>
        /// <param name="orc_ativo">Filtro por Ativo/Inativo</param>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovias</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objetos</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <returns>Lista de  de Orcamento</returns>
        public List<Orcamento> Orcamento_ListAll(int? orc_id = null, string filtroRodovia = "", string filtroObjetos = "", int? filtroStatus = -1 ,int? orc_ativo = 2
            ,string FiltroidRodovias = "", string FiltroidObjetos = "", int? usu_id = null)

        {
            try
            {
                List<Orcamento> lst = new List<Orcamento>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORCAMENTOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@orc_id", orc_id);

                    com.Parameters.AddWithValue("@filtroRodovia", filtroRodovia);
                    com.Parameters.AddWithValue("@filtroObjetos", filtroObjetos);
                    com.Parameters.AddWithValue("@filtroStatus", filtroStatus);
                    com.Parameters.AddWithValue("@orc_ativo", orc_ativo);
                    com.Parameters.AddWithValue("@FiltroidRodovias", FiltroidRodovias);
                    com.Parameters.AddWithValue("@FiltroidObjetos", FiltroidObjetos);
                    com.Parameters.AddWithValue("@usu_id", usu_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Orcamento
                        {
                            orc_id = Convert.ToInt32(rdr["orc_id"]),
                            orc_cod_orcamento = rdr["orc_cod_orcamento"].ToString(),
                            orc_descricao = rdr["orc_descricao"].ToString(),
                            orc_versao = rdr["orc_versao"].ToString(),

                            ocs_id = Convert.ToInt16(rdr["ocs_id"]),
                            ocs_codigo = rdr["ocs_codigo"].ToString(),
                            ocs_descricao = rdr["ocs_descricao"].ToString(),
                            orc_valor_total = Convert.ToDecimal(rdr["orc_valor_total"], culturePTBR),
                            orc_data_criacao = rdr["orc_data_criacao"].ToString(),
                            orc_data_validade = rdr["orc_data_validade"].ToString(),

                            orc_data_base = rdr["orc_data_base"].ToString(),
                            tpt_descricao = rdr["tpt_descricao"].ToString(),
                            tpt_id = rdr["tpt_id"].ToString(),

                            pri_ids_associados = rdr["pri_ids_associados"].ToString(),
                            orc_ord_ids_associados = rdr["orc_ord_ids_associados"].ToString(),
                            orc_os_associadas = rdr["orc_os_associadas"].ToString(),
                            orc_obj_ids_associados = rdr["orc_obj_ids_associados"].ToString(),
                            orc_objetos_associados = rdr["orc_objetos_associados"].ToString(),
                            lstStatusOrcamento = rdr["lstStatusOrcamento"].ToString(),
                            orc_ativo = Convert.ToInt16(rdr["orc_ativo"])


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
        /// Busca o proximo sequencial de Orcamento
        /// </summary>
        /// <returns>string</returns>
        public string Orcamento_ProximoSeq()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    SqlCommand com = new SqlCommand("SELECT dbo.Orcamento_ProximoSeq()", con);
                    com.Parameters.Clear();

                    string retorno = com.ExecuteScalar().ToString();

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
        ///    Insere ou Altera os dados do Orcamento no Banco
        /// </summary>
        /// <param name="orc"> Orcamento</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Orcamento_Salvar(Orcamento orc, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (orc.orc_id > 0)
                        com.CommandText = "STP_UPD_ORCAMENTO";
                    else
                        com.CommandText = "STP_INS_ORCAMENTO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (orc.orc_id > 0)
                        com.Parameters.AddWithValue("@orc_id", orc.orc_id);

                    com.Parameters.AddWithValue("@orc_cod_orcamento", orc.orc_cod_orcamento);
                    com.Parameters.AddWithValue("@orc_descricao", orc.orc_descricao);
                    com.Parameters.AddWithValue("@orc_versao", orc.orc_versao);
                    com.Parameters.AddWithValue("@ocs_id", orc.ocs_id);
                    com.Parameters.AddWithValue("@orc_data_validade", orc.orc_data_validade);
                    com.Parameters.AddWithValue("@orc_id_pai", orc.orc_id_pai);
                    com.Parameters.AddWithValue("@pri_ids_selecionados", orc.pri_ids_selecionados);

                    com.Parameters.AddWithValue("@orc_data_base", orc.orc_data_base);
                    com.Parameters.AddWithValue("@tpt_id", orc.tpt_id);

                    com.Parameters.AddWithValue("@orc_ativo", orc.orc_ativo);
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
        ///     Excluir (logicamente) Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orcamento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Orcamento_Excluir(int orc_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ORCAMENTO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@orc_id", orc_id);
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
        ///  Ativa/Desativa Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orcamento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Orcamento_AtivarDesativar(int orc_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ORCAMENTO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@orc_id", orc_id);
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
        ///    Clona os dados do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orcamento a ser clonado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Orcamento_Clonar(int orc_id, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_INS_ORCAMENTO_CLONAR";
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@orc_id", orc_id);
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

        
        // *************** ORCAMENTO_DETALHES  *************************************************************

        /// <summary>
        ///     Lista dos Detalhes do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="ore_ativo">Filtro por Elemento Ativo</param>
        /// <returns>Lista de Detalhes do Orcamento</returns>
        public List<OrcamentoDetalhes> OrcamentoDetalhes_ListAll(int orc_id, int ore_ativo)
        {
            try
            {
                int obj_id_oae_atual = -1;
                int obj_id_oae_anterior = -1;
                int obj_idElemento_atual = -1;
                int obj_idElemento_anterior = -1;

                List<OrcamentoDetalhes> lst = new List<OrcamentoDetalhes>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORCAMENTO_DETALHES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@orc_id", orc_id);
                    com.Parameters.AddWithValue("@ore_ativo", ore_ativo);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        obj_id_oae_atual = Convert.ToInt16(rdr["obj_id_oae"]);
                        obj_idElemento_atual = Convert.ToInt16(rdr["obj_idElemento"]);

                        lst.Add(new OrcamentoDetalhes
                        {
                            ore_id = Convert.ToInt32(rdr["ore_id"]),
                            orc_id = Convert.ToInt32(rdr["orc_id"]),
                            orc_cod_orcamento = rdr["orc_cod_orcamento"].ToString(),
                            orc_descricao = rdr["orc_descricao"].ToString(),
                            orc_versao = rdr["orc_versao"].ToString(),
                            orc_data_validade = rdr["orc_data_validade"].ToString(),
                            orc_valor_total = Convert.ToDecimal(rdr["orc_valor_total"], culturePTBR),
                            orc_id_pai = Convert.ToInt32(rdr["orc_id_pai"]),
                            orc_ativo = Convert.ToInt32(rdr["orc_ativo"]),

                            orc_data_base = rdr["orc_data_base"].ToString(),
                            tpt_descricao = rdr["tpt_descricao"].ToString(),
                            tpt_id = rdr["tpt_id"].ToString(),

                            ocs_id = Convert.ToInt16(rdr["ocs_id"]),
                            ocs_codigo = rdr["ocs_codigo"].ToString(),
                            ocs_descricao = rdr["ocs_descricao"].ToString(),

                            obj_id_oae = obj_id_oae_atual,
                            obj_codigoOAE = obj_id_oae_atual != obj_id_oae_anterior ? rdr["obj_codigoOAE"].ToString() : "",
                            obj_descricaoOAE = obj_id_oae_atual != obj_id_oae_anterior ? rdr["obj_descricaoOAE"].ToString() : "",

                            ore_ativo = Convert.ToInt16(rdr["ore_ativo"]),
                            ian_ativo = Convert.ToInt16(rdr["ian_ativo"]),

                            obj_idElemento = obj_idElemento_atual,
                            obj_codigoElemento = obj_idElemento_atual != obj_idElemento_anterior ? rdr["obj_codigoElemento"].ToString() : "",
                            obj_descricaoElemento =  obj_idElemento_atual != obj_idElemento_anterior ? rdr["obj_descricaoElemento"].ToString() : "",

                            ian_id = rdr["ian_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ian_id"]),
                            ian_numero = ((rdr["ian_numero"] == DBNull.Value) || (Convert.ToInt32(rdr["ian_numero"]) == 0)) ? "" : rdr["ian_numero"].ToString(),

                            ian_ordem_apresentacao = Convert.ToInt16(rdr["ian_ordem_apresentacao"]),

                            atp_id = rdr["atp_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["atp_id"]),
                            atp_codigo = rdr["atp_codigo"] == DBNull.Value ? "" : rdr["atp_codigo"].ToString(),
                            atp_descricao = rdr["atp_descricao"] == DBNull.Value ? "" : rdr["atp_descricao"].ToString(),

                            ian_sigla = rdr["ian_sigla"] == DBNull.Value ? "" : rdr["ian_sigla"].ToString(),
                            leg_id = rdr["leg_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["leg_id"]),
                            leg_codigo = rdr["leg_codigo"] == DBNull.Value ? "" : rdr["leg_codigo"].ToString(),
                            leg_descricao = rdr["leg_descricao"] == DBNull.Value ? "" : rdr["leg_descricao"].ToString(),

                            ale_id = rdr["ale_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ale_id"]),
                            ale_codigo = rdr["ale_codigo"] == DBNull.Value ? "" : rdr["ale_codigo"].ToString(),
                            ale_descricao = rdr["ale_descricao"] == DBNull.Value ? "" : rdr["ale_descricao"].ToString(),

                            aca_id = rdr["aca_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["aca_id"]),
                            aca_codigo = rdr["aca_codigo"] == DBNull.Value ? "" : rdr["aca_codigo"].ToString(),
                            aca_descricao = rdr["aca_descricao"] == DBNull.Value ? "" : rdr["aca_descricao"].ToString(),
                            ian_quantidade = rdr["ian_quantidade"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["ian_quantidade"], culturePTBR),

                            rpt_id_sugerido = rdr["rpt_id_sugerido"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["rpt_id_sugerido"]),
                            rpt_id_sugerido_codigo = rdr["rpt_id_sugerido_codigo"] == DBNull.Value ? "" : rdr["rpt_id_sugerido_codigo"].ToString(),
                            rpt_id_sugerido_descricao = rdr["rpt_id_sugerido_descricao"] == DBNull.Value ? "" : rdr["rpt_id_sugerido_descricao"].ToString(),
                            rpt_id_sugerido_unidade = rdr["rpt_id_sugerido_unidade"] == DBNull.Value ? "" : rdr["rpt_id_sugerido_unidade"].ToString(),
                            ian_quantidade_sugerida = rdr["ian_quantidade_sugerida"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["ian_quantidade_sugerida"], culturePTBR),
                            rtu_preco_unitario_sugerido = rdr["rtu_preco_unitario_sugerido"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["rtu_preco_unitario_sugerido"], culturePTBR),
                            rtu_valor_total_linha_sugerido = rdr["rtu_valor_total_linha_sugerido"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["rtu_valor_total_linha_sugerido"], culturePTBR),

                            valor_total_sugerido = rdr["valor_total_sugerido"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["valor_total_sugerido"], culturePTBR),
                            valor_total_adotado = rdr["valor_total_adotado"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["valor_total_adotado"], culturePTBR),

                            //vTotalOrcamento = rdr["vTotalOrcamento"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["vTotalOrcamento"], culturePTBR),
                            //vTotalOrcamento_Executado = rdr["vTotalOrcamento_Executado"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["vTotalOrcamento_Executado"], culturePTBR),


                            rpt_id_adotado = rdr["rpt_id_adotado"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["rpt_id_adotado"]),
                            rpt_id_adotado_codigo = rdr["rpt_id_adotado_codigo"] == DBNull.Value ? "" : rdr["rpt_id_adotado_codigo"].ToString(),
                            rpt_id_adotado_descricao = rdr["rpt_id_adotado_descricao"] == DBNull.Value ? "" : rdr["rpt_id_adotado_descricao"].ToString(),
                            rpt_id_adotado_unidade = rdr["rpt_id_adotado_unidade"] == DBNull.Value ? "" : rdr["rpt_id_adotado_unidade"].ToString(),
                            ian_quantidade_adotada = rdr["ian_quantidade_adotada"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["ian_quantidade_adotada"], culturePTBR),
                            rtu_preco_unitario_adotado = rdr["rtu_preco_unitario_adotado"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["rtu_preco_unitario_adotado"], culturePTBR),
                            rtu_valor_total_linha_adotado = rdr["rtu_valor_total_linha_adotado"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["rtu_valor_total_linha_adotado"], culturePTBR),

                            orc_objetos_associados = rdr["orc_objetos_associados"] == DBNull.Value ? "" : rdr["orc_objetos_associados"].ToString(),
                            orc_obj_ids_associados = rdr["orc_obj_ids_associados"] == DBNull.Value ? "" : rdr["orc_obj_ids_associados"].ToString(),
                            pri_ids_associados = rdr["pri_ids_associados"] == DBNull.Value ? "" : rdr["pri_ids_associados"].ToString(),
                            lstStatusOrcamento = rdr["lstStatusOrcamento"] == DBNull.Value ? "" : rdr["lstStatusOrcamento"].ToString()
                        });

                        obj_id_oae_anterior = obj_id_oae_atual;
                        obj_idElemento_anterior = obj_idElemento_atual;
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
        ///  Ativa/Desativa Orcamento
        /// </summary>
        /// <param name="ore_id">Id do Reparo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoDetalhes_AtivarDesativar(int ore_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ORCAMENTO_DETALHES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ore_id", ore_id);
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
        ///     Lista dos Serviços Adicionais por Objeto do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="obj_id">Id do Objeto que contém o serviço</param>
        /// <returns>Lista de Detalhes do Orcamento</returns>
        public List<ServicosAdicionados> Orcamento_Servicos_Adicionados_ListAll(int orc_id, int obj_id)
        {
            try
            {
                string obj_codigo_atual = "";
                string obj_codigo_anterior = "";

                List<ServicosAdicionados> lst = new List<ServicosAdicionados>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORCAMENTO_SERVICOS_ADICIONADOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@orc_id", orc_id);
                    com.Parameters.AddWithValue("@obj_id", obj_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        obj_codigo_atual = rdr["obj_codigo"].ToString();

                        lst.Add(new ServicosAdicionados
                        {
                            ose_id = Convert.ToInt16(rdr["ose_id"]),
                            orc_id = Convert.ToInt16(rdr["orc_id"]),
                            obj_id = Convert.ToInt16(rdr["obj_id"]),
                            obj_codigo = obj_codigo_atual != obj_codigo_anterior ? obj_codigo_atual : "",
                            obj_descricao = obj_codigo_atual != obj_codigo_anterior ?  rdr["obj_descricao"].ToString() : "",
                            tpt_id = rdr["tpt_id"].ToString(),
                            ose_quantidade = Convert.ToDecimal(rdr["ose_quantidade"], culturePTBR),
                            ose_quantidade_executada = rdr["ose_quantidade_executada"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["ose_quantidade_executada"], culturePTBR),
                            DataTpu = rdr["DataTpu"].ToString(),
                            ose_fase = rdr["ose_fase"].ToString(),
                            CodSubItem = rdr["CodSubItem"].ToString(),
                            NomeSubItem = rdr["NomeSubItem"].ToString(),
                            UnidMed = rdr["UnidMed"].ToString(),
                            PrecoUnit = Convert.ToDecimal(rdr["PrecoUnitario"]),
                            valor_total_linha = Convert.ToDecimal(rdr["valor_total_linha"], culturePTBR),
                            valor_total = Convert.ToDecimal(rdr["valor_total"], culturePTBR),

                            valor_total_linha_executado = rdr["valor_total_linha_executado"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["valor_total_linha_executado"], culturePTBR),
                            valor_total_executado = rdr["valor_total_executado"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["valor_total_executado"], culturePTBR),

                            Desonerado = rdr["Desonerado"].ToString(),

                            tpu_data_atualizacao = rdr["tpu_data_atualizacao"] == DBNull.Value ? "" : rdr["tpu_data_atualizacao"].ToString()
                        });

                        obj_codigo_anterior = obj_codigo_atual;
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
        ///  Excluir (logicamente) Serviço
        /// </summary>
        /// <param name="ose_id">Id do Serviço Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>JsonResult</returns>
        public int Orcamento_Servicos_Adicionados_Excluir(int ose_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ORCAMENTO_SERVICOS_ADICIONADOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ose_id", ose_id);
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
        ///  Salvar Serviços Adicionais
        /// </summary>
        /// <param name="ids_retorno">Lista dos ids alterados</param>
        /// <param name="valores_retorno">Lista dos valores alterados</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Orcamento_ServicosAdicionados_Salvar(string ids_retorno, string valores_retorno, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ORCAMENTO_SERVICOS_ADICIONADOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ids_retorno", ids_retorno);
                    com.Parameters.AddWithValue("@valores_retorno", valores_retorno);
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
        ///     Lista das TPUs a serem adicionadas
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="obj_id">Id do objeto do orcamento</param>
        /// <param name="ose_fase">Fase da TPU</param>
        /// <returns>Lista de Detalhes do Orcamento</returns>
        public List<ServicosAdicionados> OrcamentoServicosAdicionadosTPUs_ListAll(int orc_id, int obj_id, int ose_fase)
        {
            try
            {

                List<ServicosAdicionados> lst = new List<ServicosAdicionados>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORCAMENTO_SERVICOS_ADICIONADOS_TPUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@orc_id", orc_id);
                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@ose_fase", ose_fase);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new ServicosAdicionados
                        {
                            ose_id = Convert.ToInt16(rdr["ose_id"]),
                            orc_id = Convert.ToInt16(rdr["orc_id"]),
                            obj_id = Convert.ToInt16(rdr["obj_id"]),
                            obj_codigo = rdr["obj_codigo"].ToString(),
                            obj_descricao = rdr["obj_descricao"].ToString(),
                            tpt_id = rdr["tpt_id"].ToString(),
                            ose_quantidade = Convert.ToDecimal(rdr["ose_quantidade"]),
                            DataTpu = rdr["DataTpu"].ToString(),
                            ose_fase = rdr["ose_fase"].ToString(),
                            CodSubItem = rdr["CodSubItem"].ToString(),
                            NomeSubItem = rdr["NomeSubItem"].ToString(),
                            UnidMed = rdr["UnidMed"].ToString(),
                            PrecoUnit = Convert.ToDecimal(rdr["PrecoUnitario"], culturePTBR),
                            //valor_total_linha = Convert.ToDecimal(rdr["valor_total_linha"]) ,
                            //valor_total = Convert.ToDecimal(rdr["valor_total"]) ,
                            Desonerado = rdr["Desonerado"].ToString(),
                            tpu_data_atualizacao = rdr["tpu_data_atualizacao"] == DBNull.Value ? "" : rdr["tpu_data_atualizacao"].ToString()
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
        ///  Salvar Serviços Adicionais
        /// </summary>
        /// <param name="orc_id">Id do Orçamento</param>
        /// <param name="obj_id">Id do Objeto do Orçamento</param>
        /// <param name="ose_fase">Fase da TPU</param>
        /// <param name="ose_codigo_der">Código do Serviço da TPU</param>
        /// <param name="ose_quantidade">Quantidade a ser utilizada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Orcamento_Adicionar_Servico(int orc_id, int obj_id, int ose_fase, string ose_codigo_der, decimal ose_quantidade, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_INS_ORCAMENTO_SERVICO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@orc_id", orc_id);
                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@ose_fase", ose_fase);
                    com.Parameters.AddWithValue("@ose_codigo_der", ose_codigo_der);
                    com.Parameters.AddWithValue("@ose_quantidade", ose_quantidade);

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
        /// Calcula o Valor Total do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orçamento</param>
        /// <returns>decimal</returns>
        public decimal Orcamento_Total(int orc_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    SqlCommand com = new SqlCommand("SELECT dbo.fn_TotalOrcamento(" + orc_id.ToString() + ", null)", con);
                    com.Parameters.Clear();

                     decimal retorno =  Convert.ToDecimal( com.ExecuteScalar(), culturePTBR);
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
        /// Concatena Orcamentos por O.S.
        /// </summary>
        /// <param name="ord_id">Id da O.S.</param>
        /// <returns>string</returns>
        public string ConcatenaOrcamentos_por_OS(int ord_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    SqlCommand com = new SqlCommand("SELECT dbo.ConcatenaOrcamentos_por_OS(" + ord_id.ToString() + ")", con);
                    com.Parameters.Clear();

                    return com.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

        }


        // *************** STATUS  *************************************************************

        /// <summary>
        ///     Lista de todos os Status de Orcamentos não deletados
        /// </summary>
        /// <param name="ocs_id">Filtro por Id do Status de Orcamento, null para todos</param>
        /// <returns>Lista de Status de Orcamento</returns>
        public List<OrcamentoStatus> OrcamentoStatus_ListAll(int? ocs_id = null)
        {
            try
            {
                List<OrcamentoStatus> lst = new List<OrcamentoStatus>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORCAMENTO_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocs_id", ocs_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new OrcamentoStatus
                        {
                            ocs_id = Convert.ToInt32(rdr["ocs_id"]),
                            ocs_codigo = rdr["ocs_codigo"].ToString(),
                            ocs_descricao = rdr["ocs_descricao"].ToString(),
                            ocs_ativo = Convert.ToInt16(rdr["ocs_ativo"])
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
        ///    Insere ou Altera os dados do Status de Orcamento no Banco
        /// </summary>
        /// <param name="ocs">Status de Orcamento</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_Salvar(OrcamentoStatus ocs, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (ocs.ocs_id > 0)
                        com.CommandText = "STP_UPD_ORCAMENTO_STATUS";
                    else
                        com.CommandText = "STP_INS_ORCAMENTO_STATUS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (ocs.ocs_id > 0)
                        com.Parameters.AddWithValue("@ocs_id", ocs.ocs_id);

                    com.Parameters.AddWithValue("@ocs_codigo", ocs.ocs_codigo);
                    com.Parameters.AddWithValue("@ocs_descricao", ocs.ocs_descricao);
                    com.Parameters.AddWithValue("@ocs_ativo", ocs.ocs_ativo);
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
        ///     Excluir (logicamente) Status de Orcamento
        /// </summary>
        /// <param name="ocs_id">Id do Status de Orcamento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_Excluir(int ocs_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ORCAMENTO_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocs_id", ocs_id);
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
        ///  Ativa/Desativa Status de Orcamento
        /// </summary>
        /// <param name="ocs_id">Id do Status de Orcamento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_AtivarDesativar(int ocs_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ORCAMENTO_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocs_id", ocs_id);
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


        // *************** FLUXO DE STATUS  *************************************************************

        /// <summary>
        ///     Lista de Fluxo de Status de Orcamento não deletados / null para todos
        /// </summary>
        /// <param name="ocf_id">Id do Fluxo de Status de Orcamento / vazio para todos </param>
        /// <returns>Lista de OrdemServico</returns>
        public List<OrcamentoFluxoStatus> OrcamentoFluxoStatus_ListAll(int? ocf_id = null)
        {
            try
            {
                List<OrcamentoFluxoStatus> lst = new List<OrcamentoFluxoStatus>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORCAMENTO_FLUXOSTATUS", con);

                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    if (ocf_id != null)
                        com.Parameters.AddWithValue("@ocf_id", ocf_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new OrcamentoFluxoStatus
                        {
                            ocf_id = Convert.ToInt16(rdr["ocf_id"]),
                            ocf_descricao = rdr["ocf_descricao"].ToString(),
                            ocf_ativo = Convert.ToInt16(rdr["ocf_ativo"]),

                            ocs_id_de = Convert.ToInt16(rdr["ocs_id_de"]),
                            ocs_de_codigo = rdr["ocs_de_codigo"].ToString(),
                            ocs_de_descricao = rdr["ocs_de_descricao"].ToString(),

                            ocs_id_para = Convert.ToInt16(rdr["ocs_id_para"]),
                            ocs_para_codigo = rdr["ocs_para_codigo"].ToString(),
                            ocs_para_descricao = rdr["ocs_para_descricao"].ToString()
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
        ///    Insere ou Altera os dados do Fluxo de Status de Orcamento
        /// </summary>
        /// <param name="fos">Dados do Fluxo de Status de Orcamento</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_Salvar(OrcamentoFluxoStatus fos, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    if (fos.ocf_id > 0)
                        com.CommandText = "STP_UPD_ORCAMENTO_FLUXOSTATUS";
                    else
                        com.CommandText = "STP_INS_ORCAMENTO_FLUXOSTATUS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (fos.ocf_id > 0)
                        com.Parameters.AddWithValue("@ocf_id", fos.ocf_id);

                    com.Parameters.AddWithValue("@ocs_id_de", fos.ocs_id_de);
                    com.Parameters.AddWithValue("@ocs_id_para", fos.ocs_id_para);
                    com.Parameters.AddWithValue("@ocf_descricao", fos.ocf_descricao);
                    com.Parameters.AddWithValue("@ocf_ativo", fos.ocf_ativo);
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
        ///     Excluir (logicamente) Fluxo de Status de Orcamento
        /// </summary>
        /// <param name="ocf_id">Id do Fluxo de Status de Ordem de Serviço Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_Excluir(int ocf_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ORCAMENTO_FLUXOSTATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocf_id", ocf_id);
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
        ///  Ativa/Desativa Status de Orcamento
        /// </summary>
        /// <param name="ocf_id">Id do Fluxo de Status de Orcamento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_AtivarDesativar(int ocf_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ORCAMENTO_FLUXOSTATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocf_id", ocf_id);
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