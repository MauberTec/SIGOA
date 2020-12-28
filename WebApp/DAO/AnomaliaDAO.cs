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
    public class AnomaliaDAO : Conexao
    {

        // *************** LEGENDA  *************************************************************

        /// <summary>
        ///     Lista de todas as Legendas de Anomalias não deletadas
        /// </summary>
        /// <param name="leg_id">Filtro por Id da Legenda de Anomalia, null para todos</param>
        /// <returns>Lista de Legendas de Anomalias</returns>
        public List<AnomLegenda> AnomLegenda_ListAll(int? leg_id)
        {
            try
            {
                List<AnomLegenda> lst = new List<AnomLegenda>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ANOM_LEGENDA", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@leg_id", leg_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new AnomLegenda
                        {
                            leg_id = Convert.ToInt32(rdr["leg_id"]),
                            leg_codigo = rdr["leg_codigo"].ToString(),
                            leg_descricao = rdr["leg_descricao"].ToString(),
                            leg_ativo = Convert.ToInt16(rdr["leg_ativo"])
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
        ///    Insere ou Altera os dados da Legenda de Anomalia no Banco
        /// </summary>
        /// <param name="leg">Legenda de Anomalia</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomLegenda_Salvar(AnomLegenda leg, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (leg.leg_id > 0)
                        com.CommandText = "STP_UPD_ANOM_LEGENDA";
                    else
                        com.CommandText = "STP_INS_ANOM_LEGENDA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (leg.leg_id > 0)
                        com.Parameters.AddWithValue("@leg_id", leg.leg_id);

                    com.Parameters.AddWithValue("@leg_codigo", leg.leg_codigo);
                    com.Parameters.AddWithValue("@leg_descricao", leg.leg_descricao);
                    com.Parameters.AddWithValue("@leg_ativo", leg.leg_ativo);
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
        ///     Excluir (logicamente) Legenda de Anomalia
        /// </summary>
        /// <param name="leg_id">Id da Legenda de Anomalia Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomLegenda_Excluir(int leg_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ANOM_LEGENDA", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@leg_id", leg_id);
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
        ///  Ativa/Desativa Legenda de Anomalia
        /// </summary>
        /// <param name="leg_id">Id da Legenda de Anomalia Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomLegenda_AtivarDesativar(int leg_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("[STP_UPD_ATIVARDESATIVAR_ANOM_LEGENDA]", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@leg_id", leg_id);
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

        // *************** TIPO  *************************************************************

        /// <summary>
        ///     Lista de todos os Tipos de Anomalias não deletados
        /// </summary>
        /// <param name="atp_id">Filtro por Id do Tipo de Anomalia, null para todos</param>
        /// <returns>Lista de Tipo de Anomalia</returns>
        public List<AnomTipo> AnomTipo_ListAll(int? atp_id)
        {
            try
            {
                List<AnomTipo> lst = new List<AnomTipo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ANOM_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@atp_id", atp_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new AnomTipo
                        {
                            atp_id = Convert.ToInt32(rdr["atp_id"]),
                            atp_codigo = rdr["atp_codigo"].ToString(),
                            atp_descricao = rdr["atp_descricao"].ToString(),
                            atp_ativo = Convert.ToInt16(rdr["atp_ativo"])
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
        ///    Insere ou Altera os dados do Tipo de Anomalia no Banco
        /// </summary>
        /// <param name="atp">Tipo de Anomalia</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomTipo_Salvar(AnomTipo atp, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (atp.atp_id > 0)
                        com.CommandText = "STP_UPD_ANOM_TIPO";
                    else
                        com.CommandText = "STP_INS_ANOM_TIPO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (atp.atp_id > 0)
                        com.Parameters.AddWithValue("@atp_id", atp.atp_id);

                    com.Parameters.AddWithValue("@atp_codigo", atp.atp_codigo);
                    com.Parameters.AddWithValue("@atp_descricao", atp.atp_descricao);
                    com.Parameters.AddWithValue("@atp_ativo", atp.atp_ativo);
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
        ///     Excluir (logicamente) Tipo de Anomalia
        /// </summary>
        /// <param name="atp_id">Id do Tipo de Anomalia Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomTipo_Excluir(int atp_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ANOM_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@atp_id", atp_id);
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
        ///  Ativa/Desativa Tipo de Anomalia
        /// </summary>
        /// <param name="atp_id">Id do Tipo de Anomalia Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomTipo_AtivarDesativar(int atp_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("[STP_UPD_ATIVARDESATIVAR_ANOM_TIPO]", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@atp_id", atp_id);
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


        // *************** CAUSA  *************************************************************

        /// <summary>
        ///     Lista de todas as Causas de Anomalias não deletadas
        /// </summary>
        /// <param name="aca_id">Filtro por Id da Causa de Anomalia, null para todos</param>
        /// <param name="aca_descricao">Filtro por Descricao da Causa de Anomalia, vazio para todos</param>
        /// <param name="leg_id">Filtro por Legenda de Anomalia, opcional</param>
        /// <returns>Lista de Causas de Anomalias</returns>
        public List<AnomCausa> AnomCausa_ListAll(string aca_descricao, int? aca_id, int? leg_id = null)
        {
            try
            {
                List<AnomCausa> lst = new List<AnomCausa>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ANOM_CAUSA", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@aca_id", aca_id);
                    com.Parameters.AddWithValue("@leg_id", leg_id);
                    com.Parameters.AddWithValue("@aca_descricao", aca_descricao);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new AnomCausa
                        {
                            aca_id = Convert.ToInt32(rdr["aca_id"]),
                            leg_id = Convert.ToInt32(rdr["leg_id"]),
                            aca_codigo = rdr["aca_codigo"].ToString(),
                            aca_descricao = rdr["aca_descricao"].ToString(),
                            aca_ativo = Convert.ToInt16(rdr["aca_ativo"]),
                            leg_codigo = rdr["leg_codigo"].ToString(),
                            leg_descricao = rdr["leg_descricao"].ToString()
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
        ///    Insere ou Altera os dados da Causa de Anomalia no Banco
        /// </summary>
        /// <param name="aca">Causa de Anomalia</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomCausa_Salvar(AnomCausa aca, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (aca.aca_id > 0)
                        com.CommandText = "STP_UPD_ANOM_CAUSA";
                    else
                        com.CommandText = "STP_INS_ANOM_CAUSA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (aca.aca_id > 0)
                        com.Parameters.AddWithValue("@aca_id", aca.aca_id);

                    com.Parameters.AddWithValue("@leg_id", aca.leg_id);
                    com.Parameters.AddWithValue("@leg_codigo", aca.leg_codigo);
                    //com.Parameters.AddWithValue("@aca_codigo", aca.aca_codigo);
                    com.Parameters.AddWithValue("@aca_descricao", aca.aca_descricao);
                    com.Parameters.AddWithValue("@aca_ativo", aca.aca_ativo);
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
        ///     Excluir (logicamente) Causa de Anomalia
        /// </summary>
        /// <param name="aca_id">Id da Causa de Anomalia Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomCausa_Excluir(int aca_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ANOM_CAUSA", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@aca_id", aca_id);
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
        ///  Ativa/Desativa Causa de Anomalia
        /// </summary>
        /// <param name="aca_id">Id da Causa de Anomalia Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomCausa_AtivarDesativar(int aca_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("[STP_UPD_ATIVARDESATIVAR_ANOM_CAUSA]", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@aca_id", aca_id);
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



        // *************** ALERTA  *************************************************************

        /// <summary>
        ///     Lista de todos os Alertas de Anomalias não deletados
        /// </summary>
        /// <param name="ale_id">Filtro por Id do Alerta de Anomalia, null para todos</param>
        /// <returns>Lista de Alerta de Anomalia</returns>
        public List<AnomAlerta> AnomAlerta_ListAll(int? ale_id)
        {
            try
            {
                List<AnomAlerta> lst = new List<AnomAlerta>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ANOM_ALERTA", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ale_id", ale_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new AnomAlerta
                        {
                            ale_id = Convert.ToInt32(rdr["ale_id"]),
                            ale_codigo = rdr["ale_codigo"].ToString(),
                            ale_descricao = rdr["ale_descricao"].ToString(),
                            ale_ativo = Convert.ToInt16(rdr["ale_ativo"])
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
        ///    Insere ou Altera os dados do Alerta de Anomalia no Banco
        /// </summary>
        /// <param name="ale">Alerta de Anomalia</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomAlerta_Salvar(AnomAlerta ale, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (ale.ale_id > 0)
                        com.CommandText = "STP_UPD_ANOM_ALERTA";
                    else
                        com.CommandText = "STP_INS_ANOM_ALERTA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (ale.ale_id > 0)
                        com.Parameters.AddWithValue("@ale_id", ale.ale_id);

                    com.Parameters.AddWithValue("@ale_codigo", ale.ale_codigo);
                    com.Parameters.AddWithValue("@ale_descricao", ale.ale_descricao);
                    com.Parameters.AddWithValue("@ale_ativo", ale.ale_ativo);
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
        ///     Excluir (logicamente) Alerta de Anomalia
        /// </summary>
        /// <param name="ale_id">Id do Alerta de Anomalia Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomAlerta_Excluir(int ale_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ANOM_ALERTA", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ale_id", ale_id);
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
        ///  Ativa/Desativa Alerta de Anomalia
        /// </summary>
        /// <param name="ale_id">Id do Alerta de Anomalia Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomAlerta_AtivarDesativar(int ale_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("[STP_UPD_ATIVARDESATIVAR_ANOM_ALERTA]", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ale_id", ale_id);
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


        // *************** STATUS  *************************************************************

        /// <summary>
        ///     Lista de todos os Status de Anomalias não deletados
        /// </summary>
        /// <param name="ast_id">Filtro por Id do Status de Anomalia, null para todos</param>
        /// <returns>Lista de Status de Anomalia</returns>
        public List<AnomStatus> AnomStatus_ListAll(int? ast_id)
        {
            try
            {
                List<AnomStatus> lst = new List<AnomStatus>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ANOM_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ast_id", ast_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new AnomStatus
                        {
                            ast_id = Convert.ToInt32(rdr["ast_id"]),
                            ast_codigo = rdr["ast_codigo"].ToString(),
                            ast_descricao = rdr["ast_descricao"].ToString(),
                            ast_ativo = Convert.ToInt16(rdr["ast_ativo"])
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
        ///    Insere ou Altera os dados do Status de Anomalia no Banco
        /// </summary>
        /// <param name="ast">Status de Anomalia</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomStatus_Salvar(AnomStatus ast, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (ast.ast_id > 0)
                        com.CommandText = "STP_UPD_ANOM_STATUS";
                    else
                        com.CommandText = "STP_INS_ANOM_STATUS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (ast.ast_id > 0)
                        com.Parameters.AddWithValue("@ast_id", ast.ast_id);

                    com.Parameters.AddWithValue("@ast_codigo", ast.ast_codigo);
                    com.Parameters.AddWithValue("@ast_descricao", ast.ast_descricao);
                    com.Parameters.AddWithValue("@ast_ativo", ast.ast_ativo);
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
        ///     Excluir (logicamente) Status de Anomalia
        /// </summary>
        /// <param name="ast_id">Id do Status de Anomalia Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomStatus_Excluir(int ast_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ANOM_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ast_id", ast_id);
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
        ///  Ativa/Desativa Status de Anomalia
        /// </summary>
        /// <param name="ast_id">Id do Status de Anomalia Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomStatus_AtivarDesativar(int ast_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ANOM_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ast_id", ast_id);
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


        // *************** FLUXO DE STATUS  *************************************************************

        /// <summary>
        ///     Lista de Fluxo de Status de Anomalia não deletados / null para todos
        /// </summary>
        /// <param name="fst_id">Id do Fluxo de Status de Anomalia / vazio para todos </param>
        /// <returns>Lista de OrdemServico</returns>
        public List<AnomFluxoStatus> AnomFluxoStatus_ListAll(int? fst_id = null)
        {
            try
            {
                List<AnomFluxoStatus> lst = new List<AnomFluxoStatus>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ANOM_FLUXOSTATUS", con);

                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    if (fst_id != null)
                        com.Parameters.AddWithValue("@fst_id", fst_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new AnomFluxoStatus
                        {
                            fst_id = Convert.ToInt16(rdr["fst_id"]),
                            fst_descricao = rdr["fst_descricao"].ToString(),
                            fst_ativo = Convert.ToInt16(rdr["fst_ativo"]),

                            ast_id_de = Convert.ToInt16(rdr["ast_id_de"]),
                            ast_de_codigo = rdr["ast_de_codigo"].ToString(),
                            ast_de_descricao = rdr["ast_de_descricao"].ToString(),

                            ast_id_para = Convert.ToInt16(rdr["ast_id_para"]),
                            ast_para_codigo = rdr["ast_para_codigo"].ToString(),
                            ast_para_descricao = rdr["ast_para_descricao"].ToString()
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
        ///    Insere ou Altera os dados do Fluxo de Status de Anomalia
        /// </summary>
        /// <param name="fos">Dados do Fluxo de Status de Anomalia</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomFluxoStatus_Salvar(AnomFluxoStatus fos, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    if (fos.fst_id > 0)
                        com.CommandText = "STP_UPD_ANOM_FLUXOSTATUS";
                    else
                        com.CommandText = "STP_INS_ANOM_FLUXOSTATUS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (fos.fst_id > 0)
                        com.Parameters.AddWithValue("@fst_id", fos.fst_id);

                    com.Parameters.AddWithValue("@ast_id_de", fos.ast_id_de);
                    com.Parameters.AddWithValue("@ast_id_para", fos.ast_id_para);
                    com.Parameters.AddWithValue("@fst_descricao", fos.fst_descricao);
                    com.Parameters.AddWithValue("@fst_ativo", fos.fst_ativo);
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
        ///     Excluir (logicamente) Fluxo de Status de Anomalia
        /// </summary>
        /// <param name="fst_id">Id do Fluxo de Status de Ordem de Serviço Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomFluxoStatus_Excluir(int fst_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ANOM_FLUXOSTATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@fst_id", fst_id);
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
        ///  Ativa/Desativa Status de Anomalia
        /// </summary>
        /// <param name="fst_id">Id do Fluxo de Status de Anomalia Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AnomFluxoStatus_AtivarDesativar(int fst_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ANOM_FLUXOSTATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@fst_id", fst_id);
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