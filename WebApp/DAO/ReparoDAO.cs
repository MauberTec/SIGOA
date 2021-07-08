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
    public class ReparoDAO : Conexao
    {
        Conexao conn = new Conexao();

        // *************** TIPO  *************************************************************

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
        ///    Insere ou Altera os dados do Tipo de Reparo no Banco
        /// </summary>
        /// <param name="rpt">Tipo de Reparo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ReparoTipo_Salvar(ReparoTipo rpt, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (rpt.rpt_id > 0)
                        com.CommandText = "STP_UPD_REPARO_TIPO";
                    else
                        com.CommandText = "STP_INS_REPARO_TIPO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (rpt.rpt_id > 0)
                        com.Parameters.AddWithValue("@rpt_id", rpt.rpt_id);

                    com.Parameters.AddWithValue("@rpt_codigo", rpt.rpt_codigo);
                    com.Parameters.AddWithValue("@rpt_descricao", rpt.rpt_descricao);
                    com.Parameters.AddWithValue("@rpt_ativo", rpt.rpt_ativo);
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
        ///     Excluir (logicamente) Tipo de Reparo
        /// </summary>
        /// <param name="rpt_id">Id do Tipo de Reparo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ReparoTipo_Excluir(int rpt_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_REPARO_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@rpt_id", rpt_id);
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
        ///  Ativa/Desativa Tipo de Reparo
        /// </summary>
        /// <param name="rpt_id">Id do Tipo de Reparo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ReparoTipo_AtivarDesativar(int rpt_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_REPARO_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@rpt_id", rpt_id);
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




        // *************** POLITICA DE REPARO  *************************************************************

        /// <summary>
        /// Preenche Grid da Home Politica de Reparo
        /// </summary>
        /// <param name="model">Dados de Filtro</param>
        /// <returns>Lista de PoliticaReparoModel</returns>
        public List<PoliticaReparoModel> PoliticaReparo_GetbyID(PoliticaReparoModel model)
        {
           
            string ale_id = "0";
            if(model.ale_id != null)
            {
                string[] ale = model.ale_id.Split('-');
                ale_id = ale[0];
            }
            string aca_id = "0";
            if(model.aca_id != null)
            {
                string[] aca = model.aca_id.Split('-');
                aca_id = aca[0];
            }
           
            string leg_id = "0";
            if(model.leg_id != null)
            {
                string[] leg = model.leg_id.Split('-');
                leg_id = leg[0];
            }

            string atp_id = "0";
            if(model.atp_id != null)
            {
                string[] atp = model.atp_id.Split('-');
                atp_id = atp[0];
            }
           
            string rpt_id = "0";
            if(model.rpt_id != null)
            {
                string[] rpt = model.rpt_id.Split('-');
                rpt_id = rpt[0];
            }
           
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("STP_SEL_POLITICA_REPARO_PARAMETROS", con);
                com.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                com.Parameters.Add(p_return);
                com.Parameters[0].Size = 32000;

                com.Parameters.AddWithValue("@ACA_ID", aca_id);

                com.Parameters.AddWithValue("@ALE_ID", ale_id);

                com.Parameters.AddWithValue("@ATP_ID", atp_id);

                com.Parameters.AddWithValue("@RPT_ID", rpt_id);

                com.Parameters.AddWithValue("@LEG_ID", leg_id);

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PoliticaReparoModel
                    {
                        aca_codigo = reader["aca_codigo"] == DBNull.Value ? string.Empty : reader["aca_codigo"].ToString(),
                        aca_descricao = reader["aca_descricao"] == DBNull.Value ? string.Empty : reader["aca_descricao"].ToString(),
                        ale_descricao = reader["ale_descricao"] == DBNull.Value ? string.Empty : reader["ale_descricao"].ToString(),
                        ale_codigo = reader["ale_codigo"] == DBNull.Value ? string.Empty : reader["ale_codigo"].ToString(),
                        atp_descricao = reader["atp_descricao"] == DBNull.Value ? string.Empty : reader["atp_descricao"].ToString(),
                        atp_codigo = reader["atp_codigo"] == DBNull.Value ? string.Empty : reader["atp_codigo"].ToString(),
                        leg_codigo = reader["leg_codigo"] == DBNull.Value ? string.Empty : reader["leg_codigo"].ToString(),
                        leg_descricao = reader["leg_descricao"] == DBNull.Value ? string.Empty : reader["leg_descricao"].ToString(),
                        rpp_id = reader["rpp_id"] == DBNull.Value ? string.Empty : reader["rpp_id"].ToString(),
                        rpt_codigo = reader["rpt_codigo"] == DBNull.Value ? string.Empty : reader["rpt_codigo"].ToString(),
                        rpt_descricao = reader["rpt_descricao"] == DBNull.Value ? string.Empty : reader["rpt_descricao"].ToString(),
                        leg_id = reader["leg_id"] == DBNull.Value ? string.Empty : reader["leg_id"].ToString(),
                        atp_id = reader["atp_id"] == DBNull.Value ? string.Empty : reader["atp_id"].ToString(),
                        ale_id = reader["ale_id"] == DBNull.Value ? string.Empty : reader["ale_id"].ToString(),
                        aca_id = reader["aca_id"] == DBNull.Value ? string.Empty : reader["aca_id"].ToString()


                    });
                }

                
                return lista.ToList();

            }
        }

        /// <summary>
        /// Busca todos reparos
        /// </summary>
        /// <returns>Lista de PoliticaReparoModel</returns>
        public List<PoliticaReparoModel> PoliticaReparo_ListAll()
        {
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("STP_SEL_POLITICA_REPARO", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PoliticaReparoModel
                    {
                        rpp_id = reader["rpp_id"] == DBNull.Value ? string.Empty : reader["rpp_id"].ToString(),

                        aca_codigo = reader["aca_codigo"] == DBNull.Value ? string.Empty : reader["aca_codigo"].ToString(),
                        aca_descricao = reader["aca_descricao"] == DBNull.Value ? string.Empty : reader["aca_descricao"].ToString(),

                        ale_codigo = reader["ale_codigo"] == DBNull.Value ? string.Empty : reader["ale_codigo"].ToString(),
                        ale_descricao = reader["ale_descricao"] == DBNull.Value ? string.Empty : reader["ale_descricao"].ToString(),

                        atp_descricao = reader["atp_descricao"] == DBNull.Value ? string.Empty : reader["atp_descricao"].ToString(),
                        atp_codigo = reader["atp_codigo"] == DBNull.Value ? string.Empty : reader["atp_codigo"].ToString(),

                        leg_codigo = reader["leg_codigo"] == DBNull.Value ? string.Empty : reader["leg_codigo"].ToString(),
                        leg_descricao = reader["leg_descricao"] == DBNull.Value ? string.Empty : reader["leg_descricao"].ToString(),

                        rpt_codigo = reader["rpt_codigo"] == DBNull.Value ? string.Empty : reader["rpt_codigo"].ToString(),
                        rpt_descricao = reader["rpt_descricao"] == DBNull.Value ? string.Empty : reader["rpt_descricao"].ToString()
                    });
                }

            }

            return lista;
        }

        /// <summary>
        /// Deleta reparo da grid home Reparo Politica
        /// </summary>
        /// <param name="rpp_id">Id da Politica Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int PoliticaReparo_Excluir(int rpp_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_REPARO_POLITICA", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@rpp_id", rpp_id);
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
        ///  Insere Politica de Reparo
        /// </summary>
        /// <param name="model">Dados a serem inseridos</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int PoliticaReparo_Inserir(PoliticaReparoModel model, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_INS_REPARO_POLITICA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@rpt_id", model.rpt_id);

                    com.Parameters.AddWithValue("@leg_id", model.leg_id);
                    com.Parameters.AddWithValue("@ale_id", model.ale_id);
                    com.Parameters.AddWithValue("@atp_id", model.atp_id);
                    com.Parameters.AddWithValue("@aca_id", model.aca_id);

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
        /// Busca todos os Tipos de Anomalia por Legenda
        /// </summary>
        /// <returns>Lista de PoliticaReparoModel</returns>
        public List<PoliticaReparoModel> PoliticaReparo_Anomalia_By_Legenda(int leg_id)
        {
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("STP_SEL_ANOM_TIPO_BY_LEGENDA", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@leg_id", leg_id);

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PoliticaReparoModel
                    {
                        atp_id = reader["atp_id"].ToString(),
                        atp_descricao = reader["atp_descricao"].ToString(),
                        atp_codigo = reader["atp_codigo"].ToString()
                    });
                }

            }

            return lista;
        }


        /// <summary>
        /// Busca todas as Causas de Anomalia por Legenda
        /// </summary>
        /// <returns>Lista de PoliticaReparoModel</returns>
        public List<PoliticaReparoModel> PoliticaReparo_Causa_By_Legenda(int leg_id)
        {
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("STP_SEL_ANOM_CAUSA_BY_LEG_ID", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@leg_id", leg_id);

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PoliticaReparoModel
                    {
                        atp_id = reader["aca_id"].ToString(),
                        atp_descricao = reader["aca_descricao"].ToString(),
                        atp_codigo = reader["aca_codigo"].ToString()
                    });
                }

            }

            return lista;
        }


    }

}