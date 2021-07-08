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
    public class ConservaDAO : Conexao
    {
        Conexao conn = new Conexao();

        // *************** TIPO  *************************************************************

        /// <summary>
        ///     Lista de todos os Tipos de Conservas não deletados
        /// </summary>
        /// <param name="cot_id">Filtro por Id do Tipo de Conserva, null para todos</param>
        /// <returns>Lista de Tipo de Conserva</returns>
        public List<ConservaTipo> ConservaTipo_ListAll(int? cot_id)
        {
            try
            {
                List<ConservaTipo> lst = new List<ConservaTipo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_CONSERVAS_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@cot_id", cot_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new ConservaTipo
                        {
                            cot_id = Convert.ToInt32(rdr["cot_id"]),
                            cot_codigo = rdr["cot_codigo"].ToString(),
                            cot_descricao = rdr["cot_descricao"].ToString(),
                            cot_ativo = Convert.ToInt16(rdr["cot_ativo"])
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
        ///    Insere ou Altera os dados do Tipo de Conserva no Banco
        /// </summary>
        /// <param name="oct">Tipo de Conserva</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ConservaTipo_Salvar(ConservaTipo oct, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (oct.cot_id > 0)
                        com.CommandText = "STP_UPD_CONSERVA_TIPO";
                    else
                        com.CommandText = "STP_INS_CONSERVA_TIPO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (oct.cot_id > 0)
                        com.Parameters.AddWithValue("@cot_id", oct.cot_id);

                    com.Parameters.AddWithValue("@cot_codigo", oct.cot_codigo);
                    com.Parameters.AddWithValue("@cot_descricao", oct.cot_descricao);
                    com.Parameters.AddWithValue("@cot_ativo", oct.cot_ativo);
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
        ///     Excluir (logicamente) Tipo de Conserva
        /// </summary>
        /// <param name="cot_id">Id do Tipo de Conserva Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ConservaTipo_Excluir(int cot_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_CONSERVA_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@cot_id", cot_id);
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
        ///  Ativa/Desativa Tipo de Conserva
        /// </summary>
        /// <param name="cot_id">Id do Tipo de Conserva Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ConservaTipo_AtivarDesativar(int cot_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_CONSERVA_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@cot_id", cot_id);
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





        // *************** POLITICA DE Conserva  *************************************************************

        /// <summary>
        /// Busca todos Conservas
        /// </summary>
        /// <returns>Lista de Conserva_GrupoObjetos</returns>
        public List<Conserva_GrupoObjetos> PoliticaConserva_ListAll(int cot_id, string tip_nome, string cov_nome)
        {
            List<Conserva_GrupoObjetos> lista = new List<Conserva_GrupoObjetos>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("STP_SEL_CONSERVA_POLITICA", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@cot_id", cot_id);
                com.Parameters.AddWithValue("@tip_nome", tip_nome);
                com.Parameters.AddWithValue("@cov_nome", cov_nome);

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Conserva_GrupoObjetos
                    {
                        cot_id = Convert.ToInt32(reader["cot_id"]),
                        cot_codigo = reader["cot_codigo"] == DBNull.Value ? string.Empty : reader["cot_codigo"].ToString(),
                        cot_descricao = reader["cot_descricao"] == DBNull.Value ? string.Empty : reader["cot_descricao"].ToString(),
                        tip_id = Convert.ToInt32(reader["tip_id"]),
                        tip_nome = reader["tip_nome"] == DBNull.Value ? string.Empty : reader["tip_nome"].ToString(),
                        tip_pai = Convert.ToInt32(reader["tip_pai"]),
                        cop_id = Convert.ToInt32(reader["cop_id"]),
                        cov_id = Convert.ToInt32(reader["cov_id"]),
                        cov_nome = reader["cov_nome"] == DBNull.Value ? string.Empty : reader["cov_nome"].ToString(),
                        ogi_id_caracterizacao_situacao = Convert.ToInt32(reader["ogi_id_caracterizacao_situacao"]),
                        ogi_item = reader["ogi_item"] == DBNull.Value ? string.Empty : reader["ogi_item"].ToString()
                    });
                }

            }

            return lista;
        }

        /// <summary>
        /// Deleta  Politica de Conserva
        /// </summary>
        /// <param name="cop_id">Id da Politica Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int PoliticaConserva_Excluir(int cop_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_CONSERVA_POLITICA", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@cot_id", cop_id);
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
        ///  Insere os dados da Politica
        /// </summary>
        /// <param name="tip_nome">Nome do Grupo</param>
        /// <param name="lst_ogi_id_caracterizacao_situacao">Lista dos Alertas selecionados</param>
        /// <param name="lst_cov_descricao">Lista das Variaveis selecionadas</param>
        /// <param name="cot_id">Id do Tipo de Conserva</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int PoliticaConserva_Inserir(string tip_nome, string lst_ogi_id_caracterizacao_situacao, string lst_cov_descricao, int cot_id, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_INS_CONSERVA_POLITICA";
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@tip_nome", tip_nome);
                    com.Parameters.AddWithValue("@lst_ogi_id_caracterizacao_situacao", lst_ogi_id_caracterizacao_situacao);
                    com.Parameters.AddWithValue("@lst_cov_descricao", lst_cov_descricao);
                    com.Parameters.AddWithValue("@cot_id", cot_id);
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

        ///// <summary>
        /////  Salva os dados da Politica
        ///// </summary>
        ///// <param name="cop_id">Id da Politica</param>
        ///// <param name="cot_id">Id do Tipo de Conserva</param>
        ///// <param name="tip_nome">Nome do Grupo</param>
        ///// <param name="cov_id">Id da Variavel</param>
        ///// <param name="ogi_id_caracterizacao_situacao">Id do Alerta</param>
        ///// <param name="usu_id">Id do Usuário Logado</param>
        ///// <param name="ip">IP do Usuário Logado</param>
        ///// <returns>int</returns>
        //public int PoliticaConserva_Salvar(int cop_id, int cot_id, string tip_nome, int cov_id, int ogi_id_caracterizacao_situacao, int usu_id, string ip)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(strConn))
        //        {
        //            con.Open();
        //            SqlCommand com = new SqlCommand();
        //            com.CommandText = "STP_UPD_CONSERVA_POLITICA";
        //            com.Connection = con;
        //            com.CommandType = CommandType.StoredProcedure;
        //            com.Parameters.Clear();

        //            System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
        //            p_return.Direction = System.Data.ParameterDirection.ReturnValue;
        //            com.Parameters.Add(p_return);
        //            com.Parameters[0].Size = 32000;

        //            com.Parameters.AddWithValue("@cop_id", cop_id);
        //            com.Parameters.AddWithValue("@cot_id", cot_id);
        //            com.Parameters.AddWithValue("@tip_nome", tip_nome);
        //            com.Parameters.AddWithValue("@cov_id", cov_id);
        //            com.Parameters.AddWithValue("@ogi_id_caracterizacao_situacao", ogi_id_caracterizacao_situacao);

        //            com.Parameters.AddWithValue("@usu_id", usu_id);
        //            com.Parameters.AddWithValue("@ip", ip);

        //            com.ExecuteScalar();
        //            return Convert.ToInt32(p_return.Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        int id = 0;
        //        new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
        //        throw new Exception(ex.Message);
        //    }
        //}

        /// <summary>
        /// Busca todas as Variaveis de Conservas
        /// </summary>
        /// <returns>Lista de Conserva_GrupoObjetos</returns>
        public List<Conserva_GrupoObjetos> PoliticaConservaVariaveis_ListAll()
        {
            List<Conserva_GrupoObjetos> lista = new List<Conserva_GrupoObjetos>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("STP_SEL_CONSERVA_POLITICA_VARIAVEIS", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Clear();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Conserva_GrupoObjetos
                    {
                        cov_id = Convert.ToInt32(reader["cov_id"]),
                        cov_nome = reader["cov_nome"] == DBNull.Value ? string.Empty : reader["cov_nome"].ToString(),
                    });
                }

            }

            return lista;
        }

       /// <summary>
        /// Busca todas as Variaveis de Conservas pertencentes ao Grupo Selecionado
        /// </summary>
        /// <returns>Lista de Conserva_GrupoObjetos</returns>
        public List<Conserva_GrupoObjetos> PoliticaConservaVariaveis_ListAll_Tip_nome(string tip_nome)
        {
            List<Conserva_GrupoObjetos> lista = new List<Conserva_GrupoObjetos>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("STP_SEL_CONSERVA_POLITICA_VARIAVEIS_BY_TIP_NOME", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@tip_nome", tip_nome);

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Conserva_GrupoObjetos
                    {
                        cov_id = Convert.ToInt32(reader["cov_id"]),
                        cov_nome = reader["cov_nome"] == DBNull.Value ? string.Empty : reader["cov_nome"].ToString(),
                    });
                }

            }

            return lista;
        }

        /// <summary>
        /// Busca todas as Variaveis de Conservas
        /// </summary>
        /// <returns>Lista de Conserva_GrupoObjetos</returns>
        public List<Conserva_GrupoObjetos> PoliticaConservaAlerta_ListAll()
        {
            List<Conserva_GrupoObjetos> lista = new List<Conserva_GrupoObjetos>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("STP_SEL_CONSERVA_POLITICA_ALERTA", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Clear();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Conserva_GrupoObjetos
                    {
                        ogi_id_caracterizacao_situacao = Convert.ToInt32(reader["ogi_id"]),
                        ogi_item = reader["ogi_item"] == DBNull.Value ? string.Empty : reader["ogi_item"].ToString(),
                    });
                }

            }

            return lista;
        }


    }

}