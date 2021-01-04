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
    /// Perfis de Usuário
    /// </summary>
    public class PerfilDAO : Conexao
    {
        // *************** PERFIL  *************************************************************

        /// <summary>
        /// Lista de todos os Perfis 
        /// </summary>
        /// <param name="per_id">Filtro por Id de Perfil, null para todos</param> 
        /// <returns>Lista de Perfil</returns>
        public List<Perfil> Perfil_ListAll(int? per_id)
        {
            try
            {
                List<Perfil> lst = new List<Perfil>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_PERFIS", con);
                    com.CommandType = CommandType.StoredProcedure;
                     com.Parameters.AddWithValue("@per_id", per_id);

                   SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Perfil
                        {
                            per_id = Convert.ToInt32(rdr["per_id"]),
                            per_descricao = rdr["per_descricao"].ToString(),
                            per_ativo = Convert.ToInt16(rdr["per_ativo"])
                            // , per_deletado = Convert.ToDateTime(rdr["per_deletado"])
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
        /// Inclui ou Altera Perfil
        /// </summary>
        /// <param name="per">Nome do Perfil</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Perfil_Salvar(Perfil per, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (per.per_id > 0)
                        com.CommandText = "STP_UPD_PERFIL";
                    else
                        com.CommandText = "STP_INS_PERFIL";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (per.per_id > 0)
                        com.Parameters.AddWithValue("@per_id", per.per_id);

                    com.Parameters.AddWithValue("@per_descricao", per.per_descricao);
                    com.Parameters.AddWithValue("@per_ativo", per.per_ativo);
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
        /// Exclui (logicamente) o Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Perfil_Excluir(int per_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_PERFIL", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@per_id", per_id);
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
        /// Ativa/ Desativa Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Perfil_AtivarDesativar(int per_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("[STP_UPD_ATIVARDESATIVAR_PERFIL]", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@per_id", per_id);
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


        // *************** MODULOS do PERFIL *************************************************************
        //Listar Modulo do perfil
        /// <summary>
        /// Lista de todos os Módulos do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <returns>Lista de PerfilModulo</returns>
        public List<PerfilModulo> Perfil_ListAllModulos(int per_id)
        {

            try
            {
                List<PerfilModulo> lst = new List<PerfilModulo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_MODULOS_POR_PERFIL", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@per_id", per_id);
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new PerfilModulo
                        {
                            per_id = Convert.ToInt32(rdr["per_id"]),
                            mod_id = Convert.ToInt32(rdr["mod_id"]),
                            mod_nome_modulo = rdr["mod_nome_modulo"].ToString(),
                            mod_descricao = rdr["mod_descricao"].ToString(),
                            mod_ativo = Convert.ToInt16(rdr["ativo"]),
                            mfl_leitura = Convert.ToInt16(rdr["leitura"]),
                            mfl_escrita = Convert.ToInt16(rdr["escrita"]),
                            mfl_excluir = Convert.ToInt16(rdr["exclusao"]),
                            mfl_inserir = Convert.ToInt16(rdr["insercao"]),
                            mod_pai_id = Convert.ToInt16(rdr["mod_pai_id"])
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
        /// Ativa/Desativa Módulo do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="mod_id">Id do Módulo selecionado</param>
        /// <param name="mod_pai_id">Id do Módulo Pai do Módulo selecionado</param>
        /// <param name="operacao">Operação: R,W,X,I (Leitura,Escrita,Exclusão,Inserção)</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Perfil_AtivarDesativarModulo(int per_id, int mod_id, int mod_pai_id, string operacao, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_MODULOPERFIL", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@per_id", per_id);
                    com.Parameters.AddWithValue("@mod_id", mod_id);
                    com.Parameters.AddWithValue("@mod_pai_id", mod_pai_id);
                    com.Parameters.AddWithValue("@operacao", operacao);
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


        // *************** Grupos do PERFIL *************************************************************

        /// <summary>
        /// Lista de todos Grupos do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <returns>Lista de PerfilGrupo</returns>
        public List<PerfilGrupo> Perfil_ListAllGrupos(int per_id)
        {

            try
            {
                List<PerfilGrupo> lst = new List<PerfilGrupo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_GRUPOS_POR_PERFIL", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@per_id", per_id);
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new PerfilGrupo
                        {
                            per_id = per_id, // Convert.ToInt32(rdr["per_id"]),
                            gru_id = Convert.ToInt32(rdr["gru_id"]),
                            gru_descricao = rdr["gru_descricao"].ToString(),
                            gru_Associado = Convert.ToInt16(rdr["grupoAssociado"])
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
        /// Ativa/Desativa Grupo do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="gru_id">Id do Grupo selecionado</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Perfil_AtivarDesativarGrupo(int per_id, int gru_id, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_PERFILGRUPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@gru_id", gru_id);
                    com.Parameters.AddWithValue("@per_id", per_id);
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


        // ***************  USUARIOS do PERFIL *************************************************************
        /// <summary>
        /// Lista de todos os Usuários do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <returns>Lista de PerfilUsuario</returns>
        public List<PerfilUsuario> Perfil_ListAllUsuarios(int per_id)
        {
            try
            {
                List<PerfilUsuario> lst = new List<PerfilUsuario>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_USUARIOS_POR_PERFIL", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@per_id", per_id);
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new PerfilUsuario
                        {
                            per_id = Convert.ToInt32(rdr["per_id"]),
                            usu_id = Convert.ToInt32(rdr["usu_id"]),
                            usu_usuario = rdr["usu_usuario"].ToString(),
                            usu_nome = rdr["usu_nome"].ToString(),
                            usu_Associado = Convert.ToInt16(rdr["usu_Associado"])
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
        /// Ativa/Desativa Usuario do Perfil selecionado OU Perfil do Usuário Selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <param name="usu_id_logado">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Perfil_AtivarDesativarUsuario(int per_id, int usu_id, int usu_id_logado, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_PERFILUSUARIO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@per_id", per_id);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@usu_id_logado", usu_id_logado);
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