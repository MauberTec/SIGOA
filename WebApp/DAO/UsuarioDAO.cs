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
using WebApp.Business;

namespace WebApp.DAO
{
    /// <summary>
    /// Dados de Usuário
    /// </summary>
    public class UsuarioDAO : Conexao
    {

        /// <summary>
        /// Método para verificar se o usuário e senha são válidos
        /// </summary>
        /// <param name="usuario">Login do Usuário</param>
        /// <returns>Retorna os atributos do UsuarioModel</returns>
        public Usuario Usuario_ValidarLogin(Usuario usuario)
       {
            try
            {
                string ImagePath = new ParametroBLL().Parametro_GetValor("ImagePath");
                string physicalPath = System.Web.HttpContext.Current.Server.MapPath(ImagePath + "default.png");
                System.Drawing.Image image = System.Drawing.Image.FromFile(physicalPath);

                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("STP_LOGIN", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@usu_usuario", usuario.usu_usuario));
                    var senha = new Gerais().Encrypt(usuario.usu_senha);
                    cmd.Parameters.Add(new SqlParameter("@senhacrip", senha));
                    cmd.Parameters.Add(new SqlParameter("@ip", usuario.usu_ip));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Usuario _usuario = new Usuario();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                _usuario.usu_id = Convert.ToInt32(reader["usu_id"]);
                                _usuario.usu_usuario = usuario.usu_usuario;
                                _usuario.usu_senha = senha; // senha criptografada
                                _usuario.usu_ip = usuario.usu_ip;
                                _usuario.usu_nome = reader["usu_nome"].ToString();
                                _usuario.usu_ativo = Convert.ToInt32(reader["usu_ativo"]);
                                // _usuario.usu_foto = reader["usu_foto"].ToString();
                                // checa antes se existe foto
                                _usuario.usu_foto = reader["usu_foto"].ToString() == "" ? new Gerais().ImageToBase64(image) : reader["usu_foto"].ToString();

                                _usuario.usu_trocar_senha = Convert.ToInt32(reader["sen_mudar_senha"]);
                            }
                        }

                        return _usuario;
                    }
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
        ///     Busca a lista de menus que o usuário logado tem direito de acessar
        /// </summary>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <returns>DataSet</returns>
        public System.Data.DataSet Usuario_ListMenus(int usu_id)
        {
            System.Data.DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "dbo.STP_SEL_Menus";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@usu_id", System.Data.SqlDbType.Int).Value = usu_id;

                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd))
                        {
                            sqlAdapter.Fill(ds);
                        }
                        return ds;
                    }
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
        /// Altera senha de Usuário
        /// </summary>
        /// <param name="usu_id">Id do Usuário</param>
        /// <param name="usu_senha">Senha do Usuário</param>
        /// <param name="usu_id_Atualizacao">Id do Usuário logado</param>
        /// <returns>int</returns>
        public int Usuario_AlterarSenha(int usu_id, string usu_senha, int usu_id_Atualizacao)
        {

            try
            {
                var senha = new Gerais().Encrypt(usu_senha);

                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("STP_UPD_USUARIO_SENHA", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@usu_id", usu_id);
                    cmd.Parameters.AddWithValue("@pwd_senhacrip", senha);
                    cmd.Parameters.AddWithValue("@usu_id_Atualizacao", usu_id_Atualizacao);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return  Convert.ToInt32(reader["retorno"]);
                            }
                        }
                    }
                   return -1;
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
        /// Altera foto de Usuário
        /// </summary>
        /// <param name="usu_foto">nome do arquivo da foto</param>
        /// <param name="usu_id_Atualizacao">Id do Usuário logado</param>
        /// <returns>int</returns>
        public int Usuario_AlterarFoto(string usu_foto, int usu_id_Atualizacao)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            try
            {
                //string ImagePath = new ParametroBLL().Parametro_GetValor("ImagePath");

                conexao = new SqlConnection(strConn);
                cmd = new SqlCommand("STP_UPD_USUARIO_FOTO", conexao);
                
               // cmd.Parameters.AddWithValue("@usu_foto", usu_foto.Replace(ImagePath, ""));
                cmd.Parameters.AddWithValue("@usu_foto", usu_foto);
                cmd.Parameters.AddWithValue("@usu_id_logado", usu_id_Atualizacao);

                System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.Parameters.Add(p_return);
                cmd.Parameters[0].Size = 3000000; // 2Mb = 2.097.152

                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();
                cmd.ExecuteScalar();

                return Convert.ToInt32(p_return.Value);
                                
            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        /// <summary>
        /// Anula a senha corrente, cria e salva nova 
        /// </summary>
        /// <param name="usuario">Login do usuário</param>
        /// <returns>Variável do tip Usuario</returns>
        public Usuario Usuario_ResetarSenha(Usuario usuario)
       {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("STP_UPD_RESETA_SENHA", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@usu_usuario", usuario.usu_usuario));

                    cmd.Parameters.Add(new SqlParameter("@pwd_senhacrip", usuario.usu_senha));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Usuario _usuario = new Usuario();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                _usuario.usu_id = Convert.ToInt32(reader["usu_id"]);
                                _usuario.usu_usuario = usuario.usu_usuario;
                                _usuario.usu_senha = usuario.usu_senha;
                                _usuario.usu_ip = usuario.usu_ip;
                                _usuario.usu_nome = reader["usu_nome"].ToString();
                                _usuario.usu_foto = reader["usu_foto"].ToString();
                                _usuario.usu_email = reader["usu_email"].ToString();
                            }
                        }

                        return _usuario;
                    }
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
        /// Lista de permissões Módulo: Leitura, Escrita, Exclusão, Inclusão do Usuário logado
        /// </summary>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <returns>Lista de UsuarioPermissoes</returns>
        public List<UsuarioPermissoes>Usuario_ListPermissoes(int usu_id)
        {
            try
            {
                List<UsuarioPermissoes> lst = new List<UsuarioPermissoes>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("STP_SEL_PERMISSOES", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@usu_id", usu_id));

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new UsuarioPermissoes
                        {
                            mod_id = Convert.ToInt32(rdr["mod_id"]),
                           // per_id = Convert.ToInt32(rdr["per_id"]),
                            mfl_leitura = Convert.ToInt32(rdr["mfl_leitura"]),
                            mfl_escrita = Convert.ToInt32(rdr["mfl_escrita"]),
                            mfl_excluir = Convert.ToInt32(rdr["mfl_excluir"]),
                            mfl_inserir = Convert.ToInt32(rdr["mfl_inserir"])
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
        ///  Lista de todos os Usuários não deletados
        /// </summary>
        /// <param name="usu_id">Filtro por Id de Usuário, null para todos</param> 
        /// <returns> Lista de Usuario</returns>
        public List<Usuario> Usuario_ListAll(int? usu_id)
        {
            try
            {
                string ImagePath = new ParametroBLL().Parametro_GetValor("ImagePath");
                string physicalPath = System.Web.HttpContext.Current.Server.MapPath(ImagePath + "default.png");
                System.Drawing.Image image = System.Drawing.Image.FromFile(physicalPath);

                List<Usuario> lst = new List<Usuario>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_USUARIOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@usu_id", usu_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Usuario
                        {
                            usu_id = Convert.ToInt32(rdr["usu_id"]),
                            usu_usuario = rdr["usu_usuario"].ToString(),
                            usu_nome = rdr["usu_nome"].ToString(),
                            usu_email = rdr["usu_email"].ToString(),
                            usu_ativo = Convert.ToInt16(rdr["usu_ativo"]),

                            // checa antes se existe foto
                           //  usu_foto = System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(ImagePath + rdr["usu_foto"].ToString())) ? ImagePath + rdr["usu_foto"].ToString() : ImagePath + "default.png"
                         //  usu_foto =  rdr["usu_foto"].ToString()
                          usu_foto = rdr["usu_foto"].ToString() == "" ? new Gerais().ImageToBase64(image) : rdr["usu_foto"].ToString()

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
        /// Inclui ou Alterar Usuário
        /// </summary>
        /// <param name="usu">Variável do tipo USUARIO, com os novos atributos</param>
        /// <param name="usu_id_logado">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Usuario_Salvar(Usuario usu, int usu_id_logado, string ip)
        {
            try
            {
                string ImagePath = new ParametroBLL().Parametro_GetValor("ImagePath");

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (usu.usu_id > 0)
                        com.CommandText = "STP_UPD_USUARIO";
                    else
                        com.CommandText = "STP_INS_USUARIO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (usu.usu_id > 0)
                        com.Parameters.AddWithValue("@usu_id", usu.usu_id);

                    com.Parameters.AddWithValue("@usu_nome", usu.usu_nome);
                    com.Parameters.AddWithValue("@usu_usuario", usu.usu_usuario);
                    com.Parameters.AddWithValue("@usu_email", usu.usu_email);
                    if (usu.usu_foto !=null)
                        com.Parameters.AddWithValue("@usu_foto", usu.usu_foto);
                    com.Parameters.AddWithValue("@usu_ativo", usu.usu_ativo);
                    com.Parameters.AddWithValue("@usu_id_logado", usu_id_logado);
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
        /// Exclui (logicamente) Usuário
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <param name="usu_id_logado">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Usuario_Excluir(int usu_id, int usu_id_logado, string ip)
        {
            try
            {

                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_USUARIO", con);
                    com.CommandType = CommandType.StoredProcedure;
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

        /// <summary>
        /// Ativa/Desativa Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <param name="usu_id_logado">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Usuario_AtivarDesativar(int usu_id, int usu_id_logado, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("[STP_UPD_ATIVARDESATIVAR_USUARIO]", con);
                    com.CommandType = CommandType.StoredProcedure;
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


        // ***************   PERFIS do USUARIO selecionado *************************************************************
        /// <summary>
        /// Lista de Perfis do Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <returns>Lista de UsuarioPerfil</returns>
        public List<UsuarioPerfil> Usuario_ListAllPerfis(int usu_id)
        {
            try
            {
                List<UsuarioPerfil> lst = new List<UsuarioPerfil>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_PERFIS_POR_USUARIO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new UsuarioPerfil
                        {
                            usu_id = usu_id, // Convert.ToInt32(rdr["gru_id"]),
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
        /// Ativa/Desativa Perfil do Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="usu_id_logado">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Usuario_AtivarDesativarPerfil(int usu_id, int per_id, int usu_id_logado, string ip)
        {
            return new PerfilDAO().Perfil_AtivarDesativarUsuario(per_id, usu_id, usu_id_logado, ip);
        }


        // *************** Grupos do USUARIO selecionado *************************************************************
        /// <summary>
        /// Lista de Grupos do Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <returns>Lista de UsuarioGrupo</returns>
        public List<UsuarioGrupo> Usuario_ListAllGrupos(int usu_id)
        {
            try
            {
                List<UsuarioGrupo> lst = new List<UsuarioGrupo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_GRUPOS_POR_USUARIO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new UsuarioGrupo
                        {
                            usu_id = usu_id, // Convert.ToInt32(rdr["gru_id"]),
                            gru_id = Convert.ToInt32(rdr["gru_id"]),
                            gru_descricao = rdr["gru_descricao"].ToString(),
                            gru_Associado = Convert.ToInt16(rdr["gru_Associado"])
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
        /// Ativa/Desativa Grupo do Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <param name="gru_id">Id do Grupo selecionado</param>
        /// <param name="usu_id_logado">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Usuario_AtivarDesativarGrupo(int usu_id, int gru_id, int usu_id_logado, string ip)
        {
            return new GrupoDAO().Grupo_AtivarDesativarUsuario(gru_id,usu_id,  usu_id_logado, ip);
        }


    }
}