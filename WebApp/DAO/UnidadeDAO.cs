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
    /// Unidades de Perfis e/ou de Usuários
    /// </summary>
    public class UnidadeDAO : Conexao
    {

        // ********************  TIPO DE UNIDADE ***************************************************

        /// <summary>
        ///     Lista de todos os Tipos de Unidades
        /// </summary>
        /// <param name="unt_id">Filtro por Id do Tipo da Unidade, null para todos</param>
        /// <param name="unt_nome">Filtro por Nome do Tipo da Unidade</param>
        /// <returns>Lista de Unidade_Tipo</returns>
        public List<Unidade_Tipo> Unidade_Tipo_ListAll(int? unt_id=null, string unt_nome = "")
        {
            try
            {
                List<Unidade_Tipo> lst = new List<Unidade_Tipo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_UNIDADE_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@unt_id", unt_id);

                    if (unt_nome != "")
                        com.Parameters.AddWithValue("@unt_nome", unt_nome);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Unidade_Tipo
                        {
                            unt_id = Convert.ToInt16(rdr["unt_id"]),
                            unt_nome = rdr["unt_nome"].ToString(),
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
        ///    Insere ou Altera os dados do Tipo de Unidade
        /// </summary>
        /// <param name="unt">Nome do Tipo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Unidade_Tipo_Salvar(Unidade_Tipo unt, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (unt.unt_id > 0)
                        com.CommandText = "STP_UPD_UNIDADE_TIPO";
                    else
                        com.CommandText = "STP_INS_UNIDADE_TIPO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (unt.unt_id > 0)
                        com.Parameters.AddWithValue("@unt_id", unt.unt_id);

                    com.Parameters.AddWithValue("@unt_nome", unt.unt_nome);
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
        ///     Excluir (logicamente) Tipo de Unidade
        /// </summary>
        /// <param name="unt_id">Id do Tipo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Unidade_Tipo_Excluir(int unt_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_UNIDADE_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@unt_id", unt_id);
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


        // ********************  UNIDADE ***************************************************

        /// <summary>
        ///     Lista de Unidades, por Id ou por Tipo
        /// </summary>
        /// <param name="uni_id">Filtro por Id da Unidade, null para todos</param>
        /// <param name="unt_id">Filtro por Id do Tipo da Unidade, null para todos</param>
        /// <returns>Lista de Unidades</returns>
        public List<Unidade> Unidade_ListAll(int? uni_id, int? unt_id)
        {
            try
            {
                List<Unidade> lst = new List<Unidade>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_UNIDADES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@uni_id", uni_id);
                    com.Parameters.AddWithValue("@unt_id", unt_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Unidade
                        {
                            uni_id = Convert.ToInt16(rdr["uni_id"]),
                            unt_id = Convert.ToInt16(rdr["unt_id"]),
                            uni_unidade = rdr["uni_unidade"].ToString(),
                            uni_descricao = rdr["uni_descricao"].ToString(),
                            unt_nome = rdr["unt_nome"].ToString()
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
        ///    Insere ou Altera os dados da Unidade no Banco
        /// </summary>
        /// <param name="uni">Nome da Unidade</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Unidade_Salvar(Unidade uni, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (uni.uni_id > 0)
                        com.CommandText = "STP_UPD_UNIDADE";
                    else
                        com.CommandText = "STP_INS_UNIDADE";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (uni.uni_id > 0)
                        com.Parameters.AddWithValue("@uni_id", uni.uni_id);

                    com.Parameters.AddWithValue("@uni_unidade", uni.uni_unidade);
                    com.Parameters.AddWithValue("@uni_descricao", uni.uni_descricao);
                    com.Parameters.AddWithValue("@unt_id", uni.unt_id);
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
        ///     Excluir (logicamente) Unidade
        /// </summary>
        /// <param name="uni_id">Id da Unidade Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Unidade_Excluir(int uni_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_UNIDADE", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@uni_id", uni_id);
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


    }

}