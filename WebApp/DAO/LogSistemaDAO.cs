using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApp.DAO;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.DAO
{
    /// <summary>
    ///     Registros de Acesso e Alterações no Sistema
    /// </summary>
    public class LogSistemaDAO : Conexao
    {
        /// <summary>
        ///     Registra Erros e Exceções do sistema
        /// </summary>
        /// <param name="entidade">Tipo do Erro</param>
        /// <param name="id">Id do Usuário logado</param>
        /// <returns>Verdadeiro ou Falso</returns>
        public bool InserirLogErro(LogErro entidade, out int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;            

            try
            {
                conexao = new SqlConnection(strConn);
                cmd = new SqlCommand("STP_INS_LOG_EXCECAO_SISTEMA", conexao);

                cmd.Parameters.AddWithValue("@tls_tipo", entidade.tipo);
                cmd.Parameters.AddWithValue("@tls_processo", entidade.processo);
                cmd.Parameters.AddWithValue("@tls_excecao", entidade.excecao.Message);

                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                SqlParameter param = new SqlParameter("@tls_id", SqlDbType.Int); // parametro do tipo out put
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();

                id = (int)param.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conexao.Close();
            }

            return id > 0;
        }


        /// <summary>
        /// Registra ação do Usuário logado
        /// </summary>
        /// <param name="tra_id">Id da Transação (Login, Seleção, Inserção, etc)</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="mod_id">Id do Módulo acessado</param>
        /// <param name="log_texto">Texto a ser salvo no Log</param>
        /// <param name="log_ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int LogSistema_Inserir(int tra_id,
                                        string usu_id,
                                        int mod_id,
                                        string log_texto,
                                        string log_ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_INS_LOGSISTEMA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@tra_id", tra_id);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@mod_id", mod_id);
                    com.Parameters.AddWithValue("@log_texto", log_texto.Trim()== "" ? " " : log_texto);
                    com.Parameters.AddWithValue("@log_ip", log_ip);

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
        ///     Lista dos Registros de Log
        /// </summary>
        /// <param name="usu_id">Filtrar por Id do Usuário</param>
        /// <param name="data_inicio">Filtrar por Data (a partir de)</param>
        /// <param name="data_fim">Filtrar por Data (até)</param>
        /// <param name="tra_id">Filtrar por Id da Transação (Login, Seleção, Inserção, etc)</param>
        /// <param name="mod_id">Filtrar por Id do Módulo</param>
        /// <param name="texto_procurado">Filtrar por texto digitado</param>
        /// <returns>Lista de LogSistema</returns>
        public List<LogSistema> LogSistema_ListAll(int usu_id,
                                            string data_inicio,
                                            string data_fim,
                                            int tra_id,
                                            int mod_id,
                                            string texto_procurado)
        {
            try
            {
                List<LogSistema> lst = new List<LogSistema>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_LOG_SISTEMA", con);
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.AddWithValue("@usu_id", usu_id);

                    if (data_inicio != "") com.Parameters.AddWithValue("@data_inicio", data_inicio);
                    if (data_fim != "") com.Parameters.AddWithValue("@data_fim", data_fim);

                    com.Parameters.AddWithValue("@tra_id", tra_id);
                    com.Parameters.AddWithValue("@mod_id", mod_id);

                    if (texto_procurado != "") com.Parameters.AddWithValue("@texto_procurado", texto_procurado);

                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new LogSistema
                        {
                            log_id = Convert.ToInt32(rdr["log_id"]),
                            tra_id = Convert.ToInt32(rdr["tra_id"]),
                            usu_id = Convert.ToInt32(rdr["usu_id"]),
                            log_data_criacao = rdr["log_data_criacao"].ToString(),
                            mod_id = Convert.ToInt32(rdr["mod_id"]),
                            log_texto = rdr["log_texto"].ToString(),
                            log_ip = rdr["log_ip"].ToString(),

                            tra_nome = rdr["tra_nome"].ToString(),
                            mod_nome_modulo = rdr["mod_nome_modulo"].ToString(),
                            mod_descricao = rdr["mod_descricao"].ToString(),
                            usu_usuario = rdr["usu_usuario"].ToString(),
                            usu_nome = rdr["usu_nome"].ToString(),
                            log_texto_menor = rdr["log_texto"].ToString().Length >= 200 ? rdr["log_texto"].ToString().Substring(0,199) + "..." : rdr["log_texto"].ToString()
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
        /// Lista de Transações para preenchimento do Combo de Transações
        /// </summary>
        /// <returns>Lista de LogTransacao</returns>
        public List<LogTransacao> LogSistema_ListTransacao()
        {
            try
            {
                List<LogTransacao> lst = new List<LogTransacao>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_LOG_TRANSACAO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new LogTransacao
                        {
                            tra_id = Convert.ToInt32(rdr["tra_id"]),
                            tra_nome = rdr["tra_nome"].ToString()
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


    }
}