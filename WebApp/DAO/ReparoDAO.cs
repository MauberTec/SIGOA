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
        /// <summary>
        /// Preenche Grid da Home Politica de Reparo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<PoliticaReparoModel> GetReparo(PoliticaReparoModel model)
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
        /// <returns></returns>
        public List<PoliticaReparoModel> GetAllRepair()
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
                        aca_codigo = reader["aca_codigo"] == DBNull.Value ? string.Empty : reader["aca_codigo"].ToString(),
                        aca_descricao = reader["aca_descricao"] == DBNull.Value ? string.Empty : reader["aca_descricao"].ToString(),
                        ale_codigo = reader["ale_codigo"] == DBNull.Value ? string.Empty : reader["ale_codigo"].ToString(),
                        atp_descricao = reader["atp_descricao"] == DBNull.Value ? string.Empty : reader["atp_descricao"].ToString(),
                        atp_codigo = reader["atp_codigo"] == DBNull.Value ? string.Empty : reader["atp_codigo"].ToString(),
                        leg_codigo = reader["leg_codigo"] == DBNull.Value ? string.Empty : reader["leg_codigo"].ToString(),
                        leg_descricao = reader["leg_descricao"] == DBNull.Value ? string.Empty : reader["leg_descricao"].ToString(),
                        rpp_id = reader["rpp_id"] == DBNull.Value ? string.Empty : reader["rpp_id"].ToString(),
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
        /// <param name="rpp_id"></param>
        /// <returns></returns>
        public  string DelReparo(int rpp_id)
        {
            string response = string.Empty;
            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                try
                {

                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_DEL_REPARO_POLITICA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;
                    com.Parameters.AddWithValue("@rpp_id", rpp_id);

                    com.ExecuteScalar();
                    int id = Convert.ToInt32(p_return.Value);
                    response = "Reparo incluido com sucesso";
                }
                catch (Exception ex)
                {
                    response = ex.Message;
                }

            }

            return response;
        }
        /// <summary>
        /// Preeche combo home
        /// </summary>
        /// <returns></returns>
        public List<PoliticaReparoModel> GerReparo()
        {
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select * from tab_reparo_tipos order by convert(int, rpt_codigo) asc ", con);
                com.CommandType = CommandType.Text;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PoliticaReparoModel
                    {
                        rpt_id = reader["rpt_id"].ToString() + "-" + reader["rpt_codigo"].ToString(),
                        rpt_descricao = reader["rpt_descricao"].ToString(),
                        rpt_codigo = reader["rpt_codigo"].ToString()
                    });
                }
            }

            return lista;
        }
        /// <summary>
        /// Preenche Causa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PoliticaReparoModel> GetCausa(ref string id)
        {
            string[] leg = id.Split('-');
            id = leg[0];
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select * from tab_anomalia_causas where leg_id =" + id + " and aca_id <> -1 order by convert(int, aca_codigo) asc ", con);
                com.CommandType = CommandType.Text;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PoliticaReparoModel
                    {
                        aca_id = reader["aca_id"].ToString() + "-" + reader["aca_codigo"].ToString(),
                        aca_descricao = reader["aca_descricao"].ToString(),
                        aca_codigo = reader["aca_codigo"].ToString()
                    });
                }
            }

            return lista;
        }
        /// <summary>
        /// Preenche Causa
        /// </summary>
        /// <returns></returns>
        public List<PoliticaReparoModel> GetAlerta()
        {
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select * from tab_anomalia_alertas where ale_id <> -1 order by ale_codigo asc ", con);
                com.CommandType = CommandType.Text;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PoliticaReparoModel
                    {
                        ale_id = reader["ale_id"].ToString() + "-" + reader["ale_codigo"].ToString(),
                        ale_descricao = reader["ale_descricao"].ToString(),
                        ale_codigo = reader["ale_codigo"].ToString()
                    });
                }
            }

            return lista;
        }
        /// <summary>
        /// Preenche Anomalia
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PoliticaReparoModel> GetAnomalia(ref string id)
        {
            string[] leg = id.Split('-');
            id = leg[0];
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select * from tab_anomalia_tipos where leg_id = " + id + " and atp_id <> -1 order by atp_codigo asc", con);
                com.CommandType = CommandType.Text;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PoliticaReparoModel
                    {
                        atp_id = reader["atp_id"].ToString() + "-" + reader["atp_codigo"].ToString(),
                        atp_descricao = reader["atp_descricao"].ToString(),
                        atp_codigo = reader["atp_codigo"].ToString()
                    });
                }
            }

            return lista;
        }
        /// <summary>
        /// Preenche Legenda
        /// </summary>
        /// <returns></returns>m
        public List<PoliticaReparoModel> GetLegenda()
        {
            List<PoliticaReparoModel> Conserva = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(conn.strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select * from tab_anomalia_legendas where leg_id <> -1 order by leg_codigo asc ", con);
                com.CommandType = CommandType.Text;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Conserva.Add(new PoliticaReparoModel
                    {
                        Id = reader["leg_id"].ToString() + "-" + reader["leg_codigo"].ToString(),
                        leg_descricao = reader["leg_descricao"].ToString(),
                        leg_codigo = reader["leg_codigo"].ToString()
                    });
                }
            }

            return Conserva;
        }
        /// <summary>
        /// inclui um novo reparo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string InsertReparo(PoliticaReparoModel model)
        {
            string leg_codigo = "";
            string atp_codigo = "";
            string ale_codigo = "";
            string leg_id = "";
            string ale_id = "";
            string atp_id = "";
            string response = "";

            string[] leg = model.leg_codigo.Split('-');
            leg_id = leg[0];
            leg_codigo = leg[1];

            string[] ale = model.ale_codigo.Split('-');
            ale_id = ale[0];
            ale_codigo = ale[1];

            string[] atp = model.atp_codigo.Split('-');
            atp_id = atp[0];
            atp_codigo = atp[1];

            string aca_id = "";
            string[] aca = model.aca_id.Split('-');
            aca_id = aca[0];

            string rpt_id = "";
            string rpt_codigo = "";
            string[] rpt = model.rpt_id.Split('-');
            rpt_id = rpt[0];
            rpt_codigo = rpt[1];
            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    //string query = " insert into tab_reparo_politica  " +
                    //                 "(rpp_id, leg_codigo, atp_codigo, ale_codigo, aca_id, rpt_id, rpp_ativo," +
                    //                 "rpp_data_criacao, rpp_criado_por, leg_id, ale_id, atp_id) " +
                    //                 "values('" + leg_codigo + "', '" + atp_codigo + "', '" + ale_codigo + "', " + aca_id + ", " +
                    //                 "" + rpt_id + ", 1, getdate(), 4, " + leg_id + ", " + ale_id + ", " + atp_id + ")  ";
                    //com.CommandText = query;
                    com.CommandText = "STP_INS_REPARO_POLITICA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@leg_codigo", leg_codigo);
                    com.Parameters.AddWithValue("@atp_codigo", atp_codigo);
                    com.Parameters.AddWithValue("@ale_codigo", ale_codigo);
                    com.Parameters.AddWithValue("@aca_id", aca_id);

                    com.Parameters.AddWithValue("@rpt_id", rpt_id);
                    com.Parameters.AddWithValue("@rpp_ativo", 1);
                    com.Parameters.AddWithValue("@rpp_criado_por", 4);

                    com.Parameters.AddWithValue("@leg_id", leg_id);
                    com.Parameters.AddWithValue("@ale_id", ale_id);
                    com.Parameters.AddWithValue("@atp_id", atp_id);

                    com.ExecuteScalar();
                    int id = Convert.ToInt32(p_return.Value);
                    response = "0";
                }
                catch (Exception ex)
                {
                    response = "1";
                }

            }

            return response;
        }
    }

}