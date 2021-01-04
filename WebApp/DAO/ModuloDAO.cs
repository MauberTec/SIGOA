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
    /// Módulos e Telas do Sistema
    /// </summary>
    public class ModuloDAO : Conexao
    {
        /// <summary>
        /// Lista de todos os Módulos do Sistema
        /// </summary>
        /// <param name="mod_id">Filtro por Id de Módulo, null para todos</param>
        /// <returns>Lista de Módulos</returns>
        public List<Modulo> Modulo_ListAll(int? mod_id)
        {
            try
            {
                List<Modulo> lst = new List<Modulo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_MODULOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@mod_id", mod_id);

                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new Modulo
                        {
                            mod_id = Convert.ToInt32(rdr["mod_id"]),
                            mod_nome_modulo = rdr["mod_nome_modulo"].ToString(),
                            mod_descricao = rdr["mod_descricao"].ToString(),
                            mod_pai_id = Convert.ToInt16(rdr["mod_pai_id"]),
                            mod_ativo = Convert.ToInt16(rdr["mod_ativo"])
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
        /// Salva os dados do Módulo
        /// </summary>
        /// <param name="mod">Nome do Módulo</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Modulo_Salvar(Modulo mod, int usu_id, string ip)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_UPD_MODULO";
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@mod_id", mod.mod_id);
                    com.Parameters.AddWithValue("@mod_nome_modulo", mod.mod_nome_modulo);
                    com.Parameters.AddWithValue("@mod_descricao", mod.mod_descricao);
                    com.Parameters.AddWithValue("@mod_ativo", mod.mod_ativo);
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
        /// Ativa/Desativa Módulo selecionado
        /// </summary>
        /// <param name="mod_id">Id do Módulo selecionado</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Modulo_AtivarDesativar(int mod_id, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_MODULO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@mod_id", mod_id);
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