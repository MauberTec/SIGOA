using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.DAO
{
    /// <summary>
    /// 
    /// </summary>
    public class ReparoTpuDAO: Conexao
    {
        /// <summary>
        ///     Lista de todos os Tipos de Reparos não deletados
        /// </summary>
        /// <param name="rpt_id">Filtro por Id do Tipo de Reparo, null para todos</param>
        /// <returns>Lista de Tipo de Reparo</returns>
        public List<ReparoTipo> ReparoTipo_ListAll(int? rpt_id)
        {
            try
            {
                List<ReparoTipo> lst = new List<ReparoTipo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_REPARO_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@rpt_id", rpt_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new ReparoTipo
                        {
                            rpt_id = Convert.ToInt32(rdr["rpt_id"]),
                            rpt_codigo = rdr["rpt_codigo"].ToString(),
                            rpt_descricao = rdr["rpt_descricao"].ToString(),
                            rpt_ativo = Convert.ToInt16(rdr["rpt_ativo"])
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
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TpuFontesModel> GetFontes()
        {
            List<TpuFontesModel> lista = new List<TpuFontesModel>();
            using (SqlConnection con = new SqlConnection(strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select * from tab_tpu_fontes order by fon_id asc", con);
                com.CommandType = CommandType.Text;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new TpuFontesModel
                    {
                        fon_id = reader["fon_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["fon_id"].ToString()),
                        fon_nome = reader["fon_nome"].ToString()
                    });

                }
            }

            return lista;
        }

        /// <summary>
        /// Preenche Grid da Home GetReparoTpu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<TpuDtoModel> GetReparoTpu(TpuDtoModel model)
        {

            List<TpuDtoModel> lista = new List<TpuDtoModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_REPARO_TPU", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new TpuDtoModel
                        {
                            fon_nome = reader["fon_nome"] == DBNull.Value ? string.Empty : reader["fon_nome"].ToString(),
                            rtu_data_base = reader["rtu_data_base"].ToString(),
                            rpt_descricao = reader["rpt_descricao"] == DBNull.Value ? string.Empty : reader["rpt_descricao"].ToString(),
                            rtu_preco_unitario = Convert.ToDecimal(reader["rtu_preco_unitario"].ToString()),
                            rtu_codigo_tpu = reader["rtu_codigo_tpu"] == DBNull.Value ? string.Empty : reader["rtu_codigo_tpu"].ToString(),
                            rtu_id = reader["rtu_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["rtu_id"].ToString()),
                            rtu_fonte_txt = reader["rtu_fonte_txt"] == DBNull.Value ? string.Empty : reader["rtu_fonte_txt"].ToString(),
                            rpt_id = reader["rpt_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["rpt_id"].ToString()),
                            datastring = reader["datastring"] == DBNull.Value ? string.Empty : reader["datastring"].ToString(),
                            rtu_ativo = reader["rtu_ativo"] == DBNull.Value ? false : Convert.ToBoolean(reader["rtu_ativo"].ToString())
                        });
                    }
                    return lista.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        /// <summary>
        /// Preeche combo home
        /// </summary>
        /// <returns></returns>
        public List<PoliticaReparoModel> GerReparo()
        {
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select * from tab_reparo_tipos order by convert(int, rpt_codigo) asc ", con);
                com.CommandType = CommandType.Text;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PoliticaReparoModel
                    {
                        rpt_id = reader["rpt_id"].ToString(),
                        rpt_descricao = reader["rpt_descricao"].ToString(),
                        rpt_codigo = reader["rpt_codigo"].ToString()
                    });
                }
            }

            return lista;
        }

        /// <summary>
        /// DeletaConserva
        /// </summary>
        /// <param name="rtu_id"></param>
        /// <param name="ativo"></param>
        /// <returns></returns>
        public string AtualizaRtuStatu(int rtu_id, int ativo)
        {
            string response;
            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                try
                {
                    
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "update tab_reparo_tpu set rtu_ativo = "+ativo+" where rtu_id = " + rtu_id;
                    com.Connection = con;

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;
                    com.ExecuteScalar();
                    int id = Convert.ToInt32(p_return.Value);

                    response = "ok";
                }
                catch (Exception ex)
                {
                    response = ex.Message;
                }

            }

            return response;
        }

        /// <summary>
        /// AtualizaRtuEdit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AtualizaRtuEdit(TpuDtoModel model)
        {
            string response;
            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                try
                {
                    if(model.rtu_fonte_txt == null)
                    {
                        model.rtu_fonte_txt = "";
                    }
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_UPD_REPARO_TPU";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;
                    com.Parameters.AddWithValue("@rpt_id", model.rpt_id);
                    com.Parameters.AddWithValue("@fon_id", model.fon_id);
                    com.Parameters.AddWithValue("@rtu_fonte_txt", model.rtu_fonte_txt.ToString());
                    com.Parameters.AddWithValue("@rtu_codigo_tpu", model.rtu_codigo_tpu);

                    com.Parameters.AddWithValue("@rtu_preco_unitario", model.rtu_preco_unitario);
                    com.Parameters.AddWithValue("@rtu_data_base", model.rtu_data_base);
                    com.Parameters.AddWithValue("@rtu_id", model.rtu_id);
                    com.ExecuteScalar();
                    int id = Convert.ToInt32(p_return.Value);

                    response = "ok";
                }
                catch (Exception ex)
                {
                    response = ex.Message;
                }

            }

            return response;
        }
    }
}