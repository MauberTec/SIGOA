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
    /// Mao de Obra
    /// </summary>
    public class PrecoUnitarioDAO : Conexao
    {
        // *************** PrecoUnitario  *************************************************************

        /// <summary>
        ///     Lista de todos os PrecoUnitarios não deletados
        /// </summary>
        /// <param name="tpu_data_base_der">Filtro por Data Base</param>
        /// <param name="tpt_id">Filtro por Tipo Onerado/Desonerado</param>
        /// <param name="fas_id">Filtro por Fase</param>
        /// <returns>Lista de PrecoUnitarios</returns>
        public List<PrecoUnitario> PrecoUnitario_ListAll(string tpu_data_base_der, string tpt_id, int fas_id)
        {
            try
            {
                List<PrecoUnitario> lst = new List<PrecoUnitario>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_PRECOS_UNITARIOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@tpu_data_base_der", tpu_data_base_der);
                    com.Parameters.AddWithValue("@tpt_id", tpt_id);
                    com.Parameters.AddWithValue("@fas_id", fas_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new PrecoUnitario
                        {
                            tpu_id = Convert.ToInt32(rdr["tpu_id"]),
                            fas_id = Convert.ToInt32(rdr["fas_id"]),
                            fas_descricao = rdr["fas_descricao"].ToString(),
                            tpt_id = rdr["tpt_id"].ToString(),
                            tpt_descricao = rdr["tpt_descricao"].ToString(),
                            tpu_data_base_der = rdr["tpu_data_base_der"].ToString(),
                            tpu_codigo_der = rdr["tpu_codigo_der"].ToString(),
                            tpu_descricao = rdr["tpu_descricao"].ToString(),
                            uni_id = Convert.ToInt32(rdr["uni_id"]),
                            uni_unidade = rdr["uni_unidade"].ToString(),
                            moe_id = rdr["moe_id"].ToString(),
                          //  tpu_preco_unitario = Convert.ToDouble(rdr["tpu_preco_unitario"]),
                            tpu_preco_unitario = rdr["tpu_preco_unitario"].ToString(),
                            tpu_tipo_unidade = rdr["tpu_tipo_unidade"].ToString(),
                            tpu_preco_calculado = rdr["tpu_preco_calculado"].ToString(),
                            tpu_ativo = Convert.ToInt16(rdr["tpu_ativo"])
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
        ///     Lista de todas as Datas de Referência Salvas
        /// </summary>
        /// <returns>Lista de string</returns>
        public List<string> tpu_datas_base_der_ListAll()
        {
            try
            {
                List<string> lst = new List<string>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_PRECOS_UNITARIOS_DATAS_BASES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add( rdr["tpu_data_base_der"].ToString() );
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
        ///     Lista de todas as FASES
        /// </summary>
        /// <returns>Lista de PrecoUnitario_Fase</returns>
        public List<PrecoUnitario_Fase> PrecoUnitario_Fase_ListAll()
        {
            try
            {
                List<PrecoUnitario_Fase> lst = new List<PrecoUnitario_Fase>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_PRECOS_UNITARIOS_FASES", con);
                    com.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new PrecoUnitario_Fase
                        {
                            fas_id = Convert.ToInt32(rdr["fas_id"]),
                            fas_descricao = rdr["fas_descricao"].ToString(),
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