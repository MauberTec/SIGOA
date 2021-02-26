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
    /// PoliticaConservaDAO
    /// </summary>
    public class PoliticaConservaDAO : Conexao
    {

        /// <summary>
        /// Conserva
        /// </summary>
        public List<tab_conserva_tipos> GetConserva()
        {

            try
            {
                List<tab_conserva_tipos> Conserva = new List<tab_conserva_tipos>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("select * from tab_conserva_tipos", con);

                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        Conserva.Add(new tab_conserva_tipos
                        {
                           
                            cot_descricao = reader["cot_descricao"].ToString(),
                            cot_id = reader["cot_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["cot_id"]),
                        });
                    }
                    return Conserva;
                }
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }

        public class Objetos
        {
            public int tip_id { get; set; }
            public string tip_nome { get; set; }
        }

        /// <summary>
        /// Conserva
        /// </summary>
        public List<tab_conserva_variaveis> GetVariavel()
        {

            try
            {
                List<tab_conserva_variaveis> Conserva = new List<tab_conserva_variaveis>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("select * from tab_conserva_variaveis", con);

                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        Conserva.Add(new tab_conserva_variaveis
                        {
                            cov_nome = reader["cov_nome"].ToString(),
                            cov_id = reader["cov_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["cov_id"]),                            
                            cov_descricao = reader["cov_descricao"].ToString()
                        });
                    }
                    return Conserva;
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
        /// Objetos
        /// </summary>
        public List<Objetos> GetObjetos()
        {

            try
            {
                List<Objetos> lista = new List<Objetos>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand(@"select distinct ob.tip_id, ob.tip_nome, ob.tip_pai from tab_objeto_tipos ob
                                                      inner join tab_conserva_grupo_objeto_variaveis obj on obj.tip_id = ob.tip_id
                                                      where ob.tip_pai <> -1", con);

                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new Objetos
                        {
                            tip_nome = reader["tip_nome"].ToString(),
                            tip_id = reader["tip_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["tip_id"])
                        });
                    }
                    return lista;
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
        public List<PoliticaReparoModel> GetAlerta()
        {
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();
            using (SqlConnection con = new SqlConnection(strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select * from tab_anomalia_alertas where ale_id <> -1 order by ale_codigo asc ", con);
                com.CommandType = CommandType.Text;

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PoliticaReparoModel
                    {
                        ale_id = reader["ale_id"].ToString() + '-' + reader["ale_codigo"].ToString(),
                        ale_descricao = reader["ale_descricao"].ToString(),
                        ale_codigo = reader["ale_codigo"].ToString()
                    });
                }
            }

            return lista;
        }

        /// <summary>
        /// Edita conserva 
        /// </summary>
        public string EditConserva(string ocp_id, string alerta, string conserva)
        {
            string response;
            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                try
                {
                    //string query = "update tab_objeto_conserva_politica set ocp_descricao_alerta = '" + alerta + "', ocp_descricao_servico = '" + conserva + "' where ocp_id = " + ocp_id;
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_UPD_CONSERVA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@ocp_id", ocp_id);
                    com.Parameters.AddWithValue("@ocp_descricao_alerta", alerta);
                    com.Parameters.AddWithValue("@ocp_descricao_servico", conserva);
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
        /// Lista Grupos e Variaveis sem parametro
        /// </summary>
        public List<GrupoObjetosConserva> GruposConservaHome(int cot_id, int cov_id, int tip_id)
        {
            
            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                List<GrupoObjetosConserva> lista = new List<GrupoObjetosConserva>();
                con.Open();
                SqlCommand com = new SqlCommand("STP_SEL_POLITICA_CONSERVA_PARAMETROS", con);
                com.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                com.Parameters.Add(p_return);
                com.Parameters[0].Size = 32000;

                com.Parameters.AddWithValue("@cot_id", cot_id);

                com.Parameters.AddWithValue("@tip_id", tip_id);

                com.Parameters.AddWithValue("@cov_id", cov_id);

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lista.Add(new GrupoObjetosConserva
                    {                        
                        cop_id = rdr["cop_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["cop_id"]),
                        ale_id = rdr["ale_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["ale_id"]),
                        ale_codigo = rdr["ale_codigo"].ToString(),
                        cot_id = rdr["cot_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["cot_id"]),
                        cov_id = rdr["cov_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["cov_id"]),
                        tip_id = rdr["tip_id"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["tip_id"]),
                        ogi_id_caracterizacao_situacao = rdr["ogi_id_caracterizacao_situacao"].ToString(),
                        conserva = rdr["conserva"].ToString(),
                        GrupoOBJ = rdr["GrupoOBJ"].ToString(),
                        OBJPAI = rdr["OBJPAI"].ToString(),
                        Variavel = rdr["Variavel"].ToString()
                    });
                }
                return lista;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string InsertConserva(GrupoObjetosConserva model)
        {
            string response = string.Empty;
            string ale_codigo = "";
            string ale_id = "";

            string[] ale = model.ale_codigo.Split('-');
            ale_id = ale[0];
            ale_codigo = ale[1];

            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_INS_CONSERVA_TIPO";
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@cop_id", model.cop_id);
                    com.Parameters.AddWithValue("@tip_id", model.tip_id);
                    com.Parameters.AddWithValue("@cov_id", model.cop_id );
                    com.Parameters.AddWithValue("@ogi_id_caracterizacao_situacao", ale_id);
                    com.Parameters.AddWithValue("@cot_id", model.cot_id);
                    com.Parameters.AddWithValue("@cop_criado_por", 4);
                    com.Parameters.AddWithValue("@ale_id", ale_id);
                    com.Parameters.AddWithValue("@ale_codigo", ale_codigo);
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

        /// <summary>
        /// UpdateConservaTipo
        /// </summary>
        public string UpdateConservaTipo(int oct_id,string oct_codigo, string oct_descricao, string oct_ativo, string oct_criado_por)
        {
            string response;
            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                try
                {

                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_UPD_CONSERVA_TIPO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;
                    com.Parameters.AddWithValue("@oct_id", oct_id.ToString());
                    com.Parameters.AddWithValue("@oct_codigo", oct_codigo);
                    com.Parameters.AddWithValue("@oct_descricao", oct_descricao);
                    com.Parameters.AddWithValue("@oct_ativo", oct_ativo);
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
        /// Deleta conserva 
        /// </summary>
        public string DeletaConserva(int cop_id)
        {
            string response;
            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                try
                {
                    //string query = "update tab_objeto_conserva_politica set ocp_descricao_alerta = '" + alerta + "', ocp_descricao_servico = '" + conserva + "' where ocp_id = " + ocp_id;
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "delete tab_conserva_politica where cop_id = " + cop_id;
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
    }
}