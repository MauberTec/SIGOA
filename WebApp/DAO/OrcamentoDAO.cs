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
    /// OrcamentoS
    /// </summary>
    public class OrcamentoDAO : Conexao
    {
        /// <summary>
        ///     Lista de todos os Orcamentos não deletados
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="filtroRodovia">Filtro por Rodovia</param>
        /// <param name="filtroObjetos">Filtro por Objeto</param>
        /// <param name="filtroStatus">Filtro por Status</param>
        /// <returns>Lista de  de Orcamento</returns>
        public List<Orcamento> Orcamento_ListAll(int? orc_id = null, string filtroRodovia = "", string filtroObjetos = "", int? filtroStatus = -1)
        {
            try
            {
                List<Orcamento> lst = new List<Orcamento>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORCAMENTOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@orc_id", orc_id);

                    com.Parameters.AddWithValue("@filtroRodovia", filtroRodovia);
                    com.Parameters.AddWithValue("@filtroObjetos", filtroObjetos);
                    com.Parameters.AddWithValue("@filtroStatus", filtroStatus);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Orcamento
                        {
                            orc_id = Convert.ToInt32(rdr["orc_id"]),
                            orc_cod_orcamento = rdr["orc_cod_orcamento"].ToString(),
                            orc_descricao = rdr["orc_descricao"].ToString(),
                            orc_versao = rdr["orc_versao"].ToString(),

                            ocs_id = Convert.ToInt16(rdr["ocs_id"]),
                            ocs_codigo = rdr["ocs_codigo"].ToString(),
                            ocs_descricao = rdr["ocs_descricao"].ToString(),
                            orc_valor_total = Convert.ToDecimal(rdr["orc_valor_total"]),
                            orc_data_criacao = rdr["orc_data_criacao"].ToString(),
                            orc_data_validade = rdr["orc_data_validade"].ToString(),

                            orc_os_associadas = rdr["orc_os_associadas"].ToString(),
                            orc_objetos_associados = rdr["orc_objetos_associados"].ToString(),
                            orc_ativo = Convert.ToInt16(rdr["orc_ativo"])
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
        ///    Insere ou Altera os dados do Orcamento no Banco
        /// </summary>
        /// <param name="orc"> Orcamento</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Orcamento_Salvar(Orcamento orc, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (orc.orc_id > 0)
                        com.CommandText = "STP_UPD_ORCAMENTO";
                    else
                        com.CommandText = "STP_INS_ORCAMENTO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (orc.orc_id > 0)
                        com.Parameters.AddWithValue("@orc_id", orc.orc_id);

                    com.Parameters.AddWithValue("@orc_cod_orcamento", orc.orc_cod_orcamento);
                    com.Parameters.AddWithValue("@orc_descricao", orc.orc_descricao);
                    com.Parameters.AddWithValue("@orc_versao", orc.orc_versao);
                    com.Parameters.AddWithValue("@orc_data_criacao", orc.orc_data_criacao);
                    com.Parameters.AddWithValue("@orc_data_validade", orc.orc_data_validade);
                    com.Parameters.AddWithValue("@ocs_id", orc.ocs_id);
                    com.Parameters.AddWithValue("@orc_valor_total", orc.orc_valor_total);
                    com.Parameters.AddWithValue("@orc_os_associadas", orc.orc_os_associadas);
                    com.Parameters.AddWithValue("@orc_ativo", orc.orc_ativo);
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
        ///     Excluir (logicamente) Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orcamento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Orcamento_Excluir(int orc_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ORCAMENTO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@orc_id", orc_id);
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
        ///  Ativa/Desativa Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orcamento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Orcamento_AtivarDesativar(int orc_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ORCAMENTO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@orc_id", orc_id);
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
        ///     Lista de todos os Status de Orcamentos não deletados
        /// </summary>
        /// <param name="ocs_id">Filtro por Id do Status de Orcamento, null para todos</param>
        /// <returns>Lista de Status de Orcamento</returns>
        public List<OrcamentoStatus> OrcamentoStatus_ListAll(int? ocs_id = null)
        {
            try
            {
                List<OrcamentoStatus> lst = new List<OrcamentoStatus>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORCAMENTO_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocs_id", ocs_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new OrcamentoStatus
                        {
                            ocs_id = Convert.ToInt32(rdr["ocs_id"]),
                            ocs_codigo = rdr["ocs_codigo"].ToString(),
                            ocs_descricao = rdr["ocs_descricao"].ToString(),
                            ocs_ativo = Convert.ToInt16(rdr["ocs_ativo"])
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
        ///    Insere ou Altera os dados do Status de Orcamento no Banco
        /// </summary>
        /// <param name="ocs">Status de Orcamento</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_Salvar(OrcamentoStatus ocs, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (ocs.ocs_id > 0)
                        com.CommandText = "STP_UPD_ORCAMENTO_STATUS";
                    else
                        com.CommandText = "STP_INS_ORCAMENTO_STATUS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (ocs.ocs_id > 0)
                        com.Parameters.AddWithValue("@ocs_id", ocs.ocs_id);

                    com.Parameters.AddWithValue("@ocs_codigo", ocs.ocs_codigo);
                    com.Parameters.AddWithValue("@ocs_descricao", ocs.ocs_descricao);
                    com.Parameters.AddWithValue("@ocs_ativo", ocs.ocs_ativo);
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
        ///     Excluir (logicamente) Status de Orcamento
        /// </summary>
        /// <param name="ocs_id">Id do Status de Orcamento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_Excluir(int ocs_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ORCAMENTO_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocs_id", ocs_id);
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
        ///  Ativa/Desativa Status de Orcamento
        /// </summary>
        /// <param name="ocs_id">Id do Status de Orcamento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_AtivarDesativar(int ocs_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ORCAMENTO_STATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocs_id", ocs_id);
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
        ///     Lista de Fluxo de Status de Orcamento não deletados / null para todos
        /// </summary>
        /// <param name="ocf_id">Id do Fluxo de Status de Orcamento / vazio para todos </param>
        /// <returns>Lista de OrdemServico</returns>
        public List<OrcamentoFluxoStatus> OrcamentoFluxoStatus_ListAll(int? ocf_id = null)
        {
            try
            {
                List<OrcamentoFluxoStatus> lst = new List<OrcamentoFluxoStatus>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ORCAMENTO_FLUXOSTATUS", con);

                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    if (ocf_id != null)
                        com.Parameters.AddWithValue("@ocf_id", ocf_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new OrcamentoFluxoStatus
                        {
                            ocf_id = Convert.ToInt16(rdr["ocf_id"]),
                            ocf_descricao = rdr["ocf_descricao"].ToString(),
                            ocf_ativo = Convert.ToInt16(rdr["ocf_ativo"]),

                            ocs_id_de = Convert.ToInt16(rdr["ocs_id_de"]),
                            ocs_de_codigo = rdr["ocs_de_codigo"].ToString(),
                            ocs_de_descricao = rdr["ocs_de_descricao"].ToString(),

                            ocs_id_para = Convert.ToInt16(rdr["ocs_id_para"]),
                            ocs_para_codigo = rdr["ocs_para_codigo"].ToString(),
                            ocs_para_descricao = rdr["ocs_para_descricao"].ToString()
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
        ///    Insere ou Altera os dados do Fluxo de Status de Orcamento
        /// </summary>
        /// <param name="fos">Dados do Fluxo de Status de Orcamento</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_Salvar(OrcamentoFluxoStatus fos, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    if (fos.ocf_id > 0)
                        com.CommandText = "STP_UPD_ORCAMENTO_FLUXOSTATUS";
                    else
                        com.CommandText = "STP_INS_ORCAMENTO_FLUXOSTATUS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (fos.ocf_id > 0)
                        com.Parameters.AddWithValue("@ocf_id", fos.ocf_id);

                    com.Parameters.AddWithValue("@ocs_id_de", fos.ocs_id_de);
                    com.Parameters.AddWithValue("@ocs_id_para", fos.ocs_id_para);
                    com.Parameters.AddWithValue("@ocf_descricao", fos.ocf_descricao);
                    com.Parameters.AddWithValue("@ocf_ativo", fos.ocf_ativo);
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
        ///     Excluir (logicamente) Fluxo de Status de Orcamento
        /// </summary>
        /// <param name="ocf_id">Id do Fluxo de Status de Ordem de Serviço Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_Excluir(int ocf_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ORCAMENTO_FLUXOSTATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocf_id", ocf_id);
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
        ///  Ativa/Desativa Status de Orcamento
        /// </summary>
        /// <param name="ocf_id">Id do Fluxo de Status de Orcamento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_AtivarDesativar(int ocf_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ORCAMENTO_FLUXOSTATUS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ocf_id", ocf_id);
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