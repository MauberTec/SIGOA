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
    /// Grupos de Perfis e/ou de Usuários
    /// </summary>
    public class GrupoDAO : Conexao
    {
        // *************** Grupo  *************************************************************

        /// <summary>
        ///     Lista de todos os Grupos não deletados
        /// </summary>
        /// <param name="gru_id">Filtro por Id do Grupo, null para todos</param>
        /// <returns>Lista de Grupos</returns>
        public List<Grupo> Grupo_ListAll(int? gru_id)
        {
            try
            {
                List<Grupo> lst = new List<Grupo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_GRUPOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@gru_id", gru_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Grupo
                        {
                            gru_id = Convert.ToInt32(rdr["gru_id"]),
                            gru_descricao = rdr["gru_descricao"].ToString(),
                            gru_ativo = Convert.ToInt16(rdr["gru_ativo"])
                            // , gru_deletado = Convert.ToDateTime(rdr["gru_deletado"])
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
        ///    Executa Insere ou Altera os dados do Grupo no Banco
        /// </summary>
        /// <param name="gru">Nome do Grupo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Grupo_Salvar(Grupo gru, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (gru.gru_id > 0)
                        com.CommandText = "STP_UPD_GRUPO";
                    else
                        com.CommandText = "STP_INS_GRUPO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (gru.gru_id > 0)
                        com.Parameters.AddWithValue("@gru_id", gru.gru_id);

                    com.Parameters.AddWithValue("@gru_descricao", gru.gru_descricao);
                    com.Parameters.AddWithValue("@gru_ativo", gru.gru_ativo);
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
        ///     Excluir (logicamente) Grupo
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Grupo_Excluir(int gru_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_GRUPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@gru_id", gru_id);
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
        ///  Ativa/Desativa Grupo
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Grupo_AtivarDesativar(int gru_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("[STP_UPD_ATIVARDESATIVAR_GRUPO]", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@gru_id", gru_id);
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


        // *************** PERFIS do Grupo   *************************************************************
        /// <summary>
        /// Lista todos os Perfis do Grupo selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <returns>Lista de GrupoPerfil</returns>
        public List<GrupoPerfil> Grupo_ListAllPerfis(int gru_id)
        {

            try
            {
                List<GrupoPerfil> lst = new List<GrupoPerfil>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_PERFIS_POR_GRUPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@gru_id", gru_id);
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new GrupoPerfil
                        {
                            gru_id = gru_id, // Convert.ToInt32(rdr["gru_id"]),
                            per_id = Convert.ToInt32(rdr["per_id"]),
                            per_descricao = rdr["per_descricao"].ToString(),
                            per_Associado = Convert.ToInt16(rdr["per_Associado"])
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
        ///     Ativa/Desativa Perfil do Grupo selecionado OU Grupo do Perfil selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <param name="per_id">Id do Perfil Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Grupo_AtivarDesativarPerfil(int gru_id, int per_id, int usu_id, string ip)
        {
            return new PerfilDAO().Perfil_AtivarDesativarGrupo(per_id, gru_id, usu_id, ip);
        }


        // *************** USUARIOS DO GRUPO *************************************************************
        /// <summary>
        /// Lista todos os Usuários do Grupo selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <returns>Lista de GrupoUsuario</returns>
        public List<GrupoUsuario>Grupo_ListAllUsuarios(int gru_id)
        {
            try
            {
                List<GrupoUsuario> lst = new List<GrupoUsuario>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_USUARIOS_POR_GRUPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@gru_id", gru_id);
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new GrupoUsuario
                        {
                            gru_id = Convert.ToInt32(rdr["gru_id"]),
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
        ///     Ativa/Desativa Usuário do Grupo selecionado OU Grupo do Usuario Selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Selecionado</param>
        /// <param name="usu_id_logado">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Grupo_AtivarDesativarUsuario(int gru_id, int usu_id, int usu_id_logado, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_GRUPOUSUARIO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@gru_id", gru_id);
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



        // *************** Objetos Permitidos do Grupo   *************************************************************
        /// <summary>
        /// Lista todos os Objetos Permitidos do Grupo selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <returns>Lista de GrupoObjeto</returns>
        public List<GrupoObjeto> GrupoObjetos_ListAll(int gru_id)
        {

            try
            {
                List<GrupoObjeto> lst = new List<GrupoObjeto>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_GRUPO_OBJETOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@gru_id", gru_id);
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new GrupoObjeto
                        {
                            gru_id = gru_id,
                            gro_id = Convert.ToInt32(rdr["gro_id"]),
                            obj_id = Convert.ToInt32(rdr["obj_id"]),
                            obj_codigo = rdr["obj_codigo"].ToString(),
                            obj_descricao = rdr["obj_descricao"].ToString()
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
        ///   Exclui Objeto da lista de Permissões do Grupo selecionado
        /// </summary>
        /// <param name="gro_id">Id do GrupoObjeto Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int GrupoObjeto_Excluir(int gro_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_GRUPO_OBJETO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@gro_id", gro_id);
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
        ///   Acrescenta Objetos ao Grupo selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <param name="obj_ids">Ids dos Objetos a serem salvos</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        public int GrupoObjeto_Incluir(int gru_id, string obj_ids, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_INS_GRUPO_OBJETO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@gru_id", gru_id);
                    com.Parameters.AddWithValue("@obj_ids", obj_ids);
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