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
    /// NotificacaoOcorrencia
    /// </summary>
    public class NotificacaoOcorrenciaDAO : Conexao
    {
        // *************** NotificacaoOcorrencia  *************************************************************

        /// <summary>
        ///     Lista de todos as Notificacao Ocorrencias não deletados
        /// </summary>
        /// <param name="ord_id">Filtro por Id da O.S. da Notificacao de Ocorrencia, null para todos</param>
        /// <returns>Lista de NotificacaoOcorrencias</returns>
        public List<Notificacao_Ocorrencia> NotificacaoOcorrencia_ListAll(int ord_id)
        {
            try
            {
                List<Notificacao_Ocorrencia> lst = new List<Notificacao_Ocorrencia>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_NOTIFICACAO_OCORRENCIAS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ord_id", ord_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Notificacao_Ocorrencia
                        {
                            noc_id = Convert.ToInt32(rdr["noc_id"]),
                            ord_id = Convert.ToInt32(rdr["ord_id"]),
                            data_notificacao = rdr["noc_data_notificacao"].ToString(),
                            responsavel_notificacao = rdr["noc_responsavel_notificacao"].ToString(),
                            descricao_ocorrencia = rdr["noc_descricao_ocorrencia"].ToString(),
                            solicitante = rdr["noc_solicitante"].ToString(),
                            solicitante_data = rdr["noc_solicitante_data"].ToString(),
                            responsavel_recebimento = rdr["noc_responsavel_recebimento"].ToString(),
                            responsavel_recebimento_data = rdr["noc_responsavel_recebimento_data"].ToString(),

                            IdentificacaoOAE = rdr["IdentificacaoOAE"].ToString(),
                            NomeOAE = rdr["NomeOAE"].ToString(),
                            CodigoRodovia = rdr["CodigoRodovia"].ToString(),
                            NomeRodovia = rdr["NomeRodovia"].ToString(),
                            LocalizacaoKm = rdr["LocalizacaoKm"].ToString(),
                            Municipio = rdr["Municipio"].ToString(),
                            Tipo = rdr["Tipo"].ToString()
                        });
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                int id2 = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id2);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///    Insere ou Altera os dados do NotificacaoOcorrencia no Banco
        /// </summary>
        /// <param name="notOcor">Dados da Notificacao Ocorrencia</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int NotificacaoOcorrencia_Salvar(Notificacao_Ocorrencia notOcor, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (notOcor.noc_id > 0)
                        com.CommandText = "STP_UPD_NOTIFICACAO_OCORRENCIA";
                    else
                        com.CommandText = "STP_INS_NOTIFICACAO_OCORRENCIA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (notOcor.noc_id > 0)
                        com.Parameters.AddWithValue("@noc_id", notOcor.noc_id);

                    com.Parameters.AddWithValue("@ord_id", notOcor.ord_id);
                    com.Parameters.AddWithValue("@data_notificacao", notOcor.data_notificacao);
                    com.Parameters.AddWithValue("@responsavel_notificacao", notOcor.responsavel_notificacao);
                    com.Parameters.AddWithValue("@descricao_ocorrencia", notOcor.descricao_ocorrencia);
                    com.Parameters.AddWithValue("@solicitante", notOcor.solicitante);
                    com.Parameters.AddWithValue("@solicitante_data", notOcor.solicitante_data);
                    com.Parameters.AddWithValue("@responsavel_recebimento", notOcor.responsavel_recebimento);
                    com.Parameters.AddWithValue("@responsavel_recebimento_data", notOcor.responsavel_recebimento_data);

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
 
    }

}