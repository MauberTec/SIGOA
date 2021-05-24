using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
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
        CultureInfo culturePTBR = new CultureInfo("pt-BR");

        /// <summary>
        /// Busca lista de Reparos associados a TPU
        /// </summary>
        /// <returns>Lista de TpuDtoModel</returns>
        public List<TpuDtoModel> ReparoTpu_ListAll()
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
                            rtu_data_base = reader["rtu_data_base"] == DBNull.Value ? "" : reader["rtu_data_base"].ToString(),
                            rpt_descricao = reader["rpt_descricao"] == DBNull.Value ? string.Empty : reader["rpt_descricao"].ToString(),
                            rtu_preco_unitario = reader["rtu_preco_unitario"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["rtu_preco_unitario"].ToString(), culturePTBR),
                            rtu_codigo_tpu = reader["rtu_codigo_tpu"] == DBNull.Value ? string.Empty : reader["rtu_codigo_tpu"].ToString(),
                            rtu_id = reader["rtu_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["rtu_id"].ToString()),
                            rtu_fonte_txt = reader["rtu_fonte_txt"] == DBNull.Value ? string.Empty : reader["rtu_fonte_txt"].ToString(),
                            rpt_id = reader["rpt_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["rpt_id"].ToString()),
                            datastring = reader["datastring"] == DBNull.Value ? string.Empty : reader["datastring"].ToString(),
                            rtu_ativo = reader["rtu_ativo"] == DBNull.Value ? 1 : Convert.ToInt16(reader["rtu_ativo"]),
                            fon_id = reader["fon_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["fon_id"].ToString()),
                            unidade = reader["unidade"] == DBNull.Value ? string.Empty : reader["unidade"].ToString()

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
        ///  Ativa/Desativa ReparoTpu
        /// </summary>
        /// <param name="id">Id do Reparo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ReparoTpu_AtivarDesativar(int id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_REPAROTPU", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@id", id);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    i = com.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                int id22 = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id22);
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// AtualizaRtuEdit
        /// </summary>
        /// <param name="model"></param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ReparoTpu_Salvar(TpuDtoModel model, int usu_id, string ip)
        {
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

                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    com.ExecuteScalar();

                    return Convert.ToInt32(p_return.Value);

                }
                catch (Exception ex)
                {
                    int id = 0;
                    new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                    throw new Exception(ex.Message);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TpuFontesModel> ReparoTpu_GetFontesTPU()
        {
            List<TpuFontesModel> lista = new List<TpuFontesModel>();
            using (SqlConnection con = new SqlConnection(strConn))
            {
                con.Open();

                 SqlCommand com = new SqlCommand("STP_SEL_PRECOS_UNITARIOS_FONTES", con);
                 com.CommandType = CommandType.StoredProcedure;

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


    }
}