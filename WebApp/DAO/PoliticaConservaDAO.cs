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
        public List<ConservaPolitica> Conserva(int ogv_id)
        {
            try
            {
                List<ConservaPolitica> Conserva = new List<ConservaPolitica>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_CONSERVAS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@ogv_id", ogv_id);

                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        Conserva.Add(new ConservaPolitica
                        {
                            ocp_id = Convert.ToInt32(reader["ocp_id"].ToString()),
                            ogi_id_caracterizacao_situacao = Convert.ToInt32(reader["ogi_id_caracterizacao_situacao"].ToString()),
                            ocp_descricao_alerta = reader["ocp_descricao_alerta"].ToString(),
                            ocp_descricao_servico = reader["ocp_descricao_servico"].ToString()
                        }) ;
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
        /// Edita conserva da pesquisa da home
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
        public List<GruposVariaveisValores> GruposVariaveis(List<GruposVariaveisValores> lst)
        {
            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                con.Open();
                SqlCommand com = new SqlCommand("STP_SEL_OBJETO_GRUPO_OBJETO_VARIAVEIS_VALORES", con);
                com.CommandType = CommandType.StoredProcedure;

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new GruposVariaveisValores
                    {
                        numero = Convert.ToInt32(rdr["numero"]),
                        nomeGrupo = rdr["nomeGrupo"].ToString(),
                        mesclarLinhas = Convert.ToInt32(rdr["mesclarLinhas"]),
                        mesclarColunas = Convert.ToInt32(rdr["mesclarColunas"]),
                        cabecalho_Cor = rdr["cabecalho_Cor"].ToString(),
                        tip_pai = Convert.ToInt32(rdr["tip_pai"]),
                        nome_pai = rdr["nome_pai"].ToString(),
                        nCabecalhoGrupo = Convert.ToInt32(rdr["nCabecalhoGrupo"]),
                        obj_id = Convert.ToInt32(rdr["obj_id"]),
                        TemFilhos = Convert.ToInt32(rdr["TemFilhos"]),
                        tip_id_grupo = Convert.ToInt32(rdr["tip_id_grupo"]),
                        nome_grupo = rdr["nome_grupo"].ToString(),
                        ogv_id = (rdr["ogv_id"] == DBNull.Value) ? -1 : Convert.ToInt32(rdr["ogv_id"]),
                        variavel = (rdr["variavel"] == DBNull.Value) ? "" : rdr["variavel"].ToString(),
                        ogi_id_caracterizacao_situacao = Convert.ToInt32(rdr["ogi_id_caracterizacao_situacao"]),
                        ogi_id_caracterizacao_situacao_item = rdr["ogi_id_caracterizacao_situacao_item"].ToString(),
                        ati_id_condicao_inspecao = Convert.ToInt32(rdr["ati_id_condicao_inspecao"]),
                        ati_id_condicao_inspecao_item = rdr["ati_id_condicao_inspecao_item"].ToString(),
                        ovv_observacoes_gerais = rdr["ovv_observacoes_gerais"].ToString(),

                        tpu_id = Convert.ToInt32(rdr["tpu_id"]),
                        tpu_descricao = rdr["tpu_descricao"].ToString(),
                        tpu_descricao_itens_cmb = rdr["tpu_descricao_itens_cmb"].ToString(),
                        uni_id = Convert.ToInt32(rdr["uni_id"]),
                        uni_unidade = rdr["uni_unidade"].ToString(),

                        ovv_tpu_quantidade = Convert.ToDouble(rdr["ovv_tpu_quantidade"]),
                        caracterizacao_situacao_cmb = rdr["caracterizacao_situacao_cmb"].ToString(),
                        condicao_inspecao_cmb = rdr["condicao_inspecao_cmb"].ToString()

                    });
                }
                return lst;
            }
        }
    }
}