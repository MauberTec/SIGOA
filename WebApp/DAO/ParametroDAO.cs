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
    /// Parâmetros do Sistema
    /// </summary>
    public class ParametroDAO : Conexao
    {
        /// <summary>
        /// Lista de todos os Parâmetros
        /// </summary>
        /// <param name="par_id">Filtro por Id de Parâmetro, null para todos</param> 
        /// <returns>Lista de Parametro</returns>
        public List<Parametro> Parametro_ListAll(string par_id = "")
        {
            try
            {
                List<Parametro> lst = new List<Parametro>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_PARAMETROS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@par_id", par_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Parametro
                        {
                            par_id = rdr["par_id"].ToString(),
                            par_valor = rdr["par_valor"].ToString(),
                            par_descricao = rdr["par_descricao"].ToString(),
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
        /// Salva Parâmetro
        /// </summary>
        /// <param name="par">Nome do Parâmetro</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Parametro_Salvar(Parametro par, int usu_id, string ip)
        {
            try
            {
                var par_valor = par.par_valor;
                if (par.par_id == "email_Senha")
                    par_valor =  new Gerais().Encrypt(par.par_valor);

                    using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "STP_UPD_PARAMETRO";
                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@par_id", par.par_id);

                    com.Parameters.AddWithValue("@par_valor", par_valor);
                    com.Parameters.AddWithValue("@par_descricao", par.par_descricao);
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
        /// Lista de Parâmetros de Email
        /// </summary>
        /// <returns>Lista de ParamsEmail</returns>
        public List<ParamsEmail> Parametro_ListAllParamsEmail()
        {
            List<ParamsEmail> lst = new List<ParamsEmail>();
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_PARAMETROS_EMAIL", con);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new ParamsEmail
                        {
                            // De = rdr["email_Nome_Sistema"].ToString() + "<" + rdr["email_De"].ToString() + ">",
                            Enviar_Emails = Convert.ToInt32(rdr["email_Enviar_Emails"].ToString()),
                            De = rdr["email_De"].ToString(),
                            Para = "",
                            Assunto = "",
                            Texto = rdr["email_txtEsqueciSenha"].ToString(),
                            Anexo = "",
                            CC = "",
                            CCO = "",
                            SMTPServer = rdr["email_SmtpServer"].ToString(),
                            PortaSmtp = Convert.ToInt32(rdr["email_PortaSmtp"].ToString()),
                            EnableSSL = Convert.ToBoolean(rdr["email_EnableSSL"].ToString()),
                            Timeout = Convert.ToInt32(rdr["email_Timeout"].ToString()),
                            IsBodyHtml = Convert.ToBoolean(rdr["email_IsBodyHtml"].ToString()),
                            Usuario = rdr["email_Usuario"].ToString(),
                            Senha = rdr["email_Senha"].ToString().Trim() != "" ? new Gerais().Decrypt(rdr["email_Senha"].ToString().Trim(), "G4WedT") : "",
                            Dominio = rdr["email_Dominio"].ToString(),
                            URL_SISTEMA = rdr["email_URL_SISTEMA"].ToString()
                        });
                        break;
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                lst.Add(new ParamsEmail
                { De = ex.ToString()
                });
                return lst;
            }

        }


        /// <summary>
        /// Busca o valor do Parâmetro solicitado
        /// </summary>
        /// <param name="par_id">Nome do Parâmetro</param>
        /// <returns>Valor do Parâmetro solicitado</returns>
        public string Parametro_GetValor(string par_id)
        {
            try
            {
                List<Parametro> lst = new List<Parametro>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_PARAMETRO_ID", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@par_id", par_id);

                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        return rdr["par_valor"].ToString();
                    }
                }

                return "";
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