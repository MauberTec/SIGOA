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
    /// Objetos de Perfis e/ou de Usuários
    /// </summary>
    public class ObjetoDAO : Conexao
    {
        // *************** Objeto  *************************************************************

        /// <summary>
        ///     Lista de todos os Objetos não deletados
        /// </summary>
        /// <param name="obj_id">Filtro por Id do Objeto, 0 para todos</param> 
        /// <param name="filtro_obj_codigo">Filtro por codigo de Objeto, null para todos</param> 
        /// <param name="filtro_obj_descricao">Filtro por descrição de Objeto, null para todos</param> 
        /// <param name="filtro_clo_id">Filtro por classe de Objeto, -1 para todos</param> 
        /// <param name="filtro_tip_id">Filtro por tipo de Objeto, -1 para todos</param> 
        /// <returns>Lista de Objetos</returns>
        public List<Objeto> Objeto_ListAll(int obj_id, string filtro_obj_codigo = null, string filtro_obj_descricao = null, int? filtro_clo_id = -1, int? filtro_tip_id = -1)
        {
            try
            {
                List<Objeto> lst = new List<Objeto>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@filtro_obj_codigo", filtro_obj_codigo);
                    com.Parameters.AddWithValue("@filtro_obj_descricao", filtro_obj_descricao);
                    if ((filtro_clo_id >=0) || (filtro_clo_id == -13)) // -13  retorna classe 2 e 3
                        com.Parameters.AddWithValue("@filtro_clo_id", filtro_clo_id);

                    if (filtro_tip_id >= 0)
                    com.Parameters.AddWithValue("@filtro_tip_id", filtro_tip_id);




                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Objeto
                        {
                            row_numero = Convert.ToDouble(rdr["row_numero"]),
                            row_expandida = Convert.ToDouble(rdr["row_expandida"]),
                            nNivel = Convert.ToDouble(rdr["nNivel"]),
                            temFilhos = Convert.ToInt32(rdr["temFilhos"]),
                            obj_id = Convert.ToInt32(rdr["obj_id"]),
                            clo_id = Convert.ToInt32(rdr["clo_id"]),
                            tip_id = Convert.ToInt32(rdr["tip_id"]),
                            obj_codigo = rdr["obj_codigo"].ToString(),
                            obj_descricao = rdr["obj_descricao"].ToString(),
                            obj_organizacao = (rdr["obj_organizacao"] == DBNull.Value) ? string.Empty : rdr["obj_organizacao"].ToString(),
                            obj_departamento = (rdr["obj_departamento"] == DBNull.Value) ? string.Empty : rdr["obj_departamento"].ToString(),
                            obj_status = (rdr["obj_status"] == DBNull.Value) ? string.Empty : rdr["obj_status"].ToString(),
                            obj_arquivo_kml = (rdr["obj_arquivo_kml"] == DBNull.Value) ? string.Empty : rdr["obj_arquivo_kml"].ToString(),
                            obj_ativo = Convert.ToInt16(rdr["obj_ativo"]),
                            clo_nome = rdr["clo_nome"].ToString(),
                            tip_nome = rdr["tip_nome"].ToString(),
                            obj_podeDeletar = Convert.ToInt16(rdr["obj_podeDeletar"]),
                            obj_pai = (rdr["obj_pai"] == DBNull.Value) ? -1 : Convert.ToInt16(rdr["obj_pai"])
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
        /// Busca os dados do Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <returns>Lista de "Objeto"</returns>
        public List<Objeto> Objeto_GetbyID(int obj_id)
        {
            try
            {
                List<Objeto> lst = new List<Objeto>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETOS_ID", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@obj_id", obj_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Objeto
                        {
                            obj_id = Convert.ToInt32(rdr["obj_id"]),
                            clo_id = Convert.ToInt32(rdr["clo_id"]),
                            tip_id = Convert.ToInt32(rdr["tip_id"]),
                            obj_codigo = rdr["obj_codigo"].ToString(),
                            obj_descricao = rdr["obj_descricao"].ToString(),
                            obj_organizacao = (rdr["obj_organizacao"] == DBNull.Value) ? string.Empty : rdr["obj_organizacao"].ToString(),
                            obj_departamento = (rdr["obj_departamento"] == DBNull.Value) ? string.Empty : rdr["obj_departamento"].ToString(),
                            obj_status = (rdr["obj_status"] == DBNull.Value) ? string.Empty : rdr["obj_status"].ToString(),
                            obj_arquivo_kml = (rdr["obj_arquivo_kml"] == DBNull.Value) ? string.Empty : rdr["obj_arquivo_kml"].ToString(),
                            obj_ativo = Convert.ToInt16(rdr["obj_ativo"]),
                            obj_pai = (rdr["obj_pai"] == DBNull.Value) ? -1 : Convert.ToInt16(rdr["obj_pai"])
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
        ///    Altera os dados do Objeto no Banco
        /// </summary>
        /// <param name="obj_id">Id do Objeto</param>
        /// <param name="obj_codigo">Código do Objeto</param>
        /// <param name="obj_descricao">Descrição do Objeto (opcional)</param>
        /// <param name="tip_id">Id do Tipo do Objeto (opcional)</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Objeto_Salvar(int obj_id, string obj_codigo, string obj_descricao, int tip_id, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "STP_UPD_OBJETO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@obj_codigo", obj_codigo);
                    com.Parameters.AddWithValue("@obj_descricao", obj_descricao);
                    com.Parameters.AddWithValue("@tip_id", tip_id);
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
        ///  Insere Novo Objeto pelo Código
        /// </summary>
        /// <param name="obj_codigo">Código do Objeto</param>
        /// <param name="obj_descricao">Descrição do Objeto (opcional)</param>
        /// <param name="obj_NumeroObjetoAte">No caso de inserção de item Número Objeto, é possível inserção em lote</param>
        /// <param name="obj_localizacaoAte">No caso de inserção de item Localização, é possível inserção em lote</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>string</returns>
        public string Objeto_Inserir(string obj_codigo, string obj_descricao, string obj_NumeroObjetoAte, string obj_localizacaoAte, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_INS_OBJETO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    //System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    //p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    //com.Parameters.Add(p_return);
                    //com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@obj_codigo", obj_codigo);
                    com.Parameters.AddWithValue("@obj_descricao", obj_descricao);
                    com.Parameters.AddWithValue("@obj_NumeroObjetoAte", obj_NumeroObjetoAte);
                    com.Parameters.AddWithValue("@obj_localizacaoAte", obj_localizacaoAte);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    //com.ExecuteScalar();
                    // return Convert.ToInt32(p_return.Value);
                    // return p_return.Value.ToString();

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        return rdr["saida"].ToString();
                    }

                    return "";
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
        /// Exclui Objeto do tipo Subdivisao2 (encontro/ estrutura de terra; encontros/ estrutura de concreto)
        /// </summary>
        /// <param name="tip_id">Id do tipo do Objeto Selecionado</param>
        /// <param name="obj_id_tipoOAE">Id do Objeto Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>string</returns>
        public string Objeto_Subdivisao2_Excluir(int tip_id, int obj_id_tipoOAE, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_OBJETO_SUBDIVISAO2", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@tip_id", tip_id);
                    com.Parameters.AddWithValue("@obj_id_tipoOAE", obj_id_tipoOAE);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    i = com.ExecuteNonQuery();
                }
                return i >0 ? "" : "Erro";
            }
            catch (Exception ex)
            {
                // int id = 0;
                // new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                // throw new Exception(ex.Message);
                return ex.Message;
            }
        }


        /// <summary>
        ///     Excluir (logicamente) Objeto
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Objeto_Excluir(int obj_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_OBJETO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@obj_id", obj_id);
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
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Objeto_AtivarDesativar(int obj_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_OBJETO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@obj_id", obj_id);
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
        ///    Associa Documentos ao Objeto selecionado
        /// </summary>
        /// <param name="doc_ids">Ids dos Documentos Selecionados</param>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Objeto_AssociarDocumentos(string doc_ids, int obj_id, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_OBJ_ASSOCIAR_DOCUMENTOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@doc_ids", doc_ids);
                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@usu_id_logado", usu_id);
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
        ///    Desassocia Documento do Objeto selecionado
        /// </summary>
        /// <param name="doc_id">Id dos Documento Selecionado</param>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Objeto_DesassociarDocumento(int doc_id, int obj_id, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_OBJ_DESASSOCIAR_DOCUMENTO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@doc_id", doc_id);
                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@usu_id_logado", usu_id);
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

        // -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// Lista de todos os Documentos Associados ao Objeto selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <returns>Lista de Documentos</returns>
        public List<Documento> Objeto_Documentos_ListAll(int obj_id)
        {
            try
            {
                string CaminhoVirtualRaizArquivos = new ParametroDAO().Parametro_GetValor("CaminhoVirtualRaizArquivos");
                List<Documento> lst = new List<Documento>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETO_DOCUMENTOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@obj_id", obj_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        string doc_caminho = rdr["doc_caminho"].ToString();
                        if (doc_caminho.Trim() != "")
                        {
                            if (new Gerais().RemoteFileExists(CaminhoVirtualRaizArquivos + doc_caminho))
                                doc_caminho = CaminhoVirtualRaizArquivos + doc_caminho;
                            else
                                doc_caminho = "";
                        }

                        lst.Add(new Documento
                        {
                            doc_id = Convert.ToInt32(rdr["doc_id"]),
                            doc_codigo = rdr["doc_codigo"].ToString(),
                            doc_descricao = rdr["doc_descricao"].ToString(),
                            tpd_id = rdr["tpd_id"].ToString(),
                            doc_caminho = doc_caminho,
                            doc_ativo = Convert.ToInt16(rdr["doc_ativo"]),
                            tpd_descricao = rdr["tpd_descricao"].ToString(),
                            dos_referencia = Convert.ToInt16(rdr["dos_referencia"])
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
        ///  Lista de todos os Objetos não associados para o Documento Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <param name="codDoc">Codigo ou parte do Documento a procurar</param>
        /// <returns>Lista de Documentos Nao Associados</returns>
        public List<Documento> Objeto_DocumentosNaoAssociados_ListAll(int obj_id, string codDoc)
        {
            try
            {
                List<Documento> lst = new List<Documento>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETO_DOCUMENTOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@doc_codigo", codDoc);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Documento
                        {
                            doc_id = Convert.ToInt32(rdr["doc_id"]),
                            doc_codigo = rdr["doc_codigo"].ToString(),
                            doc_descricao = rdr["doc_descricao"].ToString(),
                            tpd_id = rdr["tpd_id"].ToString(),
                            doc_caminho = rdr["doc_caminho"].ToString(),
                            doc_ativo = Convert.ToInt16(rdr["doc_ativo"]),
                            tpd_descricao = rdr["tpd_descricao"].ToString(),
                            total_registros = Convert.ToInt32(rdr["total_registros"])
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

        // -----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Lista de Objeto Localizacao
        /// </summary>
        /// <param name="obj_id_TipoOAE">Id do Objeto do Tipo OAE</param> 
        /// <param name="tip_id_Grupo">Id do tipo do Grupo de Objeto</param> 
        /// <returns>Lista de Objetos</returns>
        public List<Objeto> Objeto_Localizacao_ListAll(int obj_id_TipoOAE, int tip_id_Grupo)
        {
            try
            {
                List<Objeto> lst = new List<Objeto>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETOS_LOCALIZACAO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@obj_id_TipoOAE", obj_id_TipoOAE);
                    com.Parameters.AddWithValue("@tip_id_Grupo", tip_id_Grupo);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Objeto
                        {
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




        // *************** CLASSES DE Objeto  *************************************************************

        /// <summary>
        ///     Lista de todas as Classes não deletadas
        /// </summary>
        /// <param name="clo_id">Filtro por Id da Classe de Objeto, null para todos</param> 
        /// <returns>Lista de ObjClasse</returns>
        public List<ObjClasse> ObjClasse_ListAll(int? clo_id)
        {
            try
            {
                List<ObjClasse> lst = new List<ObjClasse>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJ_CLASSES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@clo_id", clo_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new ObjClasse
                        {
                            clo_id = Convert.ToInt32(rdr["clo_id"]),
                            clo_nome = rdr["clo_nome"].ToString(),
                            clo_descricao = rdr["clo_descricao"].ToString(),
                            clo_ativo = Convert.ToInt16(rdr["clo_ativo"])
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
        ///    Executa Insere ou Altera os dados da Classe no Banco
        /// </summary>
        /// <param name="objClasse">Dados da Classe</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjClasse_Salvar(ObjClasse objClasse, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (objClasse.clo_id > 0)
                        com.CommandText = "STP_UPD_OBJ_CLASSE";
                    else
                        com.CommandText = "STP_INS_OBJ_CLASSE";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (objClasse.clo_id > 0)
                        com.Parameters.AddWithValue("@clo_id", objClasse.clo_id);

                    com.Parameters.AddWithValue("@clo_nome", objClasse.clo_nome);
                    com.Parameters.AddWithValue("@clo_descricao", objClasse.clo_descricao);
                    com.Parameters.AddWithValue("@clo_ativo", objClasse.clo_ativo);
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
        ///     Excluir (logicamente) Objeto
        /// </summary>
        /// <param name="clo_id">Id da Classe Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjClasse_Excluir(int clo_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_OBJ_CLASSE", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@clo_id", clo_id);
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
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="clo_id">Id da Classe Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjClasse_AtivarDesativar(int clo_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("[STP_UPD_ATIVARDESATIVAR_OBJ_CLASSE]", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@clo_id", clo_id);
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



        // *************** TIPOS DE Objeto  *************************************************************

        /// <summary>
        ///     Lista de todos os Tipos não deletados
        /// </summary>
        /// <param name="clo_id">Id da Classe Selecionada</param>
        /// <param name="tip_id">Id do Tipo Selecionado</param>
        /// <param name="tip_pai">Id do Tipo Pai</param>
        /// <param name="excluir_existentes">Menos os valores já existentes</param>
        /// <param name="obj_id">Id do objeto selecionado</param>
        /// <returns>Lista de ObjTipo</returns>
        public List<ObjTipo> ObjTipo_ListAll(int? clo_id, int? tip_id, int? tip_pai= 0, int? excluir_existentes = 0, int? obj_id = 0)
        {
            try
            {
                List<ObjTipo> lst = new List<ObjTipo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJ_TIPOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@clo_id", clo_id);
                    com.Parameters.AddWithValue("@tip_id", tip_id);
                    com.Parameters.AddWithValue("@tip_pai", tip_pai);
                    com.Parameters.AddWithValue("@excluir_existentes", excluir_existentes);
                    com.Parameters.AddWithValue("@obj_id", obj_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new ObjTipo
                        {
                            tip_id = Convert.ToInt32(rdr["tip_id"]),
                            clo_id = Convert.ToInt32(rdr["clo_id"]),
                            tip_codigo = rdr["tip_codigo"].ToString(),
                            tip_nome = rdr["tip_nome"].ToString(),
                            tip_descricao = rdr["tip_descricao"].ToString(),
                           // tip_mascara_codificacao = rdr["tip_mascara_codificacao"].ToString(),
                            tip_ativo = Convert.ToInt16(rdr["tip_ativo"]),
                            tem_var_inspecao = Convert.ToInt16(rdr["tem_var_inspecao"])
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
        ///    Executa Insere ou Altera os dados do Tipo no Banco
        /// </summary>
        /// <param name="objTipo">Dados do Tipo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjTipo_Salvar(ObjTipo objTipo, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (objTipo.tip_id > 0)
                        com.CommandText = "STP_UPD_OBJ_TIPO";
                    else
                        com.CommandText = "STP_INS_OBJ_TIPO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (objTipo.tip_id > 0)
                        com.Parameters.AddWithValue("@tip_id", objTipo.tip_id);

                    com.Parameters.AddWithValue("@clo_id", objTipo.clo_id);
                    com.Parameters.AddWithValue("@tip_codigo", objTipo.tip_codigo);
                    com.Parameters.AddWithValue("@tip_nome", objTipo.tip_nome);
                    com.Parameters.AddWithValue("@tip_descricao", objTipo.tip_descricao);
                    com.Parameters.AddWithValue("@tip_ativo", objTipo.tip_ativo);
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
        ///     Excluir (logicamente) Objeto
        /// </summary>
        /// <param name="tip_id">Id do Tipo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjTipo_Excluir(int tip_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_OBJ_TIPO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@tip_id", tip_id);
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
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="tip_id">Id do Tipo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjTipo_AtivarDesativar(int tip_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("[STP_UPD_ATIVARDESATIVAR_OBJ_TIPO]", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@tip_id", tip_id);
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




        // *************** ATRIBUTOS DE OBJETO  *************************************************************

        /// <summary>
        ///     Lista de todos os Atributos não deletados
        /// </summary>
        /// <param name="atr_id">Filtro por Id de Atributo</param>
        /// <param name="filtro_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtro_descricao">Descrição ou Parte a se localizar</param>
        /// <param name="filtro_clo_id">Id da Classe a se filtrar</param>
        /// <param name="filtro_tip_id">Id do Tipo a se filtrar</param>
        /// <param name="ehAtributoFuncional">Flag de atributo funcional</param>
        /// <returns>Lista de ObjAtributo</returns>
        public List<ObjAtributo> ObjAtributo_ListAll(int atr_id, string filtro_codigo, string filtro_descricao, int filtro_clo_id, int filtro_tip_id, int ehAtributoFuncional)
        {
            try
            {
                List<ObjAtributo> lst = new List<ObjAtributo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ATRIBUTOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    com.Parameters.AddWithValue("@atr_id", atr_id);

                    com.Parameters.AddWithValue("@filtro_codigo", filtro_codigo);
                    com.Parameters.AddWithValue("@filtro_descricao", filtro_descricao);
                    com.Parameters.AddWithValue("@filtro_clo_id", filtro_clo_id);
                    com.Parameters.AddWithValue("@filtro_tip_id", filtro_tip_id);
                    com.Parameters.AddWithValue("@atr_atributo_funcional", ehAtributoFuncional);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new ObjAtributo
                        {
                            atr_id = Convert.ToInt32(rdr["atr_id"]),
                            tip_id = Convert.ToInt32(rdr["tip_id"]),
                            clo_id = Convert.ToInt32(rdr["clo_id"]),
                            atr_atributo_nome = rdr["atr_atributo_nome"].ToString(),
                            atr_descricao = rdr["atr_descricao"].ToString(),
                            atr_mascara_texto = rdr["atr_mascara_texto"].ToString(),
                            atr_ativo = Convert.ToInt16(rdr["atr_ativo"]),
                            atr_herdavel = Convert.ToInt16(rdr["atr_herdavel"]),
                            atr_atributo_funcional = (rdr["atr_atributo_funcional"] == DBNull.Value) ? 0 : Convert.ToInt16(rdr["atr_atributo_funcional"]),
                            clo_nome = rdr["clo_nome"].ToString(),
                            tip_nome = rdr["tip_nome"].ToString(),
                            atr_itens_ids = rdr["atr_itens_ids"].ToString(),
                            atr_itens_codigo = rdr["atr_itens_codigo"].ToString(),
                            atr_itens_descricao = rdr["atr_itens_descricao"].ToString(),
                            atr_apresentacao_itens = (rdr["atr_apresentacao_itens"] == DBNull.Value) ? "" : rdr["atr_apresentacao_itens"].ToString()
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
        ///    Executa Insere ou Altera os dados do ATRIBUTO no Banco
        /// </summary>
        /// <param name="objAtributo">Dados do ATRIBUTO</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjAtributo_Salvar(ObjAtributo objAtributo, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (objAtributo.atr_id > 0)
                        com.CommandText = "STP_UPD_ATRIBUTO";
                    else
                        com.CommandText = "STP_INS_ATRIBUTO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@atr_id", objAtributo.atr_id);
                    com.Parameters.AddWithValue("@tip_id", objAtributo.tip_id);
                    com.Parameters.AddWithValue("@clo_id", objAtributo.clo_id);
                    com.Parameters.AddWithValue("@atr_atributo_nome", objAtributo.atr_atributo_nome);
                    com.Parameters.AddWithValue("@atr_descricao", objAtributo.atr_descricao);
                    com.Parameters.AddWithValue("@atr_mascara_texto", objAtributo.atr_mascara_texto);
                    com.Parameters.AddWithValue("@atr_herdavel", objAtributo.atr_herdavel);
                    com.Parameters.AddWithValue("@atr_ativo", objAtributo.atr_ativo);
                    com.Parameters.AddWithValue("@atr_atributo_funcional", objAtributo.atr_atributo_funcional);

                    com.Parameters.AddWithValue("@atr_apresentacao_itens", objAtributo.atr_apresentacao_itens);

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
        ///     Excluir (logicamente) ATRIBUTO
        /// </summary>
        /// <param name="atr_id">Id do ATRIBUTO Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjAtributo_Excluir(int atr_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ATRIBUTO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;
                    com.Parameters.AddWithValue("@atr_id", atr_id);
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
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="atr_id">Id do Atributo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjAtributo_AtivarDesativar(int atr_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("[STP_UPD_ATIVARDESATIVAR_ATRIBUTO]", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@atr_id", atr_id);
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



        // *************** ITENS DE ATRIBUTO DE Objeto  *************************************************************

        /// <summary>
        ///     Lista de todos os ITENS DE ATRIBUTO não deletados
        /// </summary>
        /// <param name="atr_id">Id do ATRIBUTO Selecionado</param>
        /// <param name="ati_id">Id do Item do ATRIBUTO Selecionado</param>
        /// <returns>Lista de ObjAtributoItem</returns>
        public List<ObjAtributoItem> ObjAtributoItem_ListAll(int atr_id, int? ati_id = null)
        {
            try
            {
                List<ObjAtributoItem> lst = new List<ObjAtributoItem>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_ATRIBUTOS_ITENS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@atr_id", atr_id);
                    com.Parameters.AddWithValue("@ati_id", ati_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new ObjAtributoItem
                        {
                            ati_id = Convert.ToInt32(rdr["ati_id"]),
                            atr_id = Convert.ToInt32(rdr["atr_id"]),
                            ati_item = rdr["ati_item"].ToString(),
                            ati_ativo = Convert.ToInt16(rdr["ati_ativo"])
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
        ///    Executa Insere ou Altera os dados do ITEM de ATRIBUTO no Banco
        /// </summary>
        /// <param name="objAtributoItem">Dados do ITEM de ATRIBUTO</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjAtributoItem_Salvar(ObjAtributoItem objAtributoItem, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (objAtributoItem.ati_id > 0)
                        com.CommandText = "STP_UPD_ATRIBUTO_ITEM";
                    else
                        com.CommandText = "STP_INS_ATRIBUTO_ITEM";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (objAtributoItem.ati_id > 0)
                        com.Parameters.AddWithValue("@ati_id", objAtributoItem.ati_id);

                    com.Parameters.AddWithValue("@atr_id", objAtributoItem.atr_id);
                    com.Parameters.AddWithValue("@ati_item", objAtributoItem.ati_item);
                    com.Parameters.AddWithValue("@ati_ativo", objAtributoItem.ati_ativo);
                    com.Parameters.AddWithValue("@atr_atributo_funcional", objAtributoItem.atr_atributo_funcional);

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
        ///     Excluir (logicamente) ITEM de ATRIBUTO
        /// </summary>
        /// <param name="ati_id">Id do ITEM do ATRIBUTO Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjAtributoItem_Excluir(int ati_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_ATRIBUTO_ITEM", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ati_id", ati_id);
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
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="ati_id">Id do ITEM do ATRIBUTO Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjAtributoItem_AtivarDesativar(int ati_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_ATRIBUTO_ITEM", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ati_id", ati_id);
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



        // *************** VALORES DE ATRIBUTO  *************************************************************
        /// <summary>
        /// Lista os Atributos do Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <param name="atr_id">Id do ATRIBUTO selecionado</param>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param>
        /// <returns>JsonResult Lista de ObjAtributoValores</returns>
        public List<ObjAtributoValores> ObjAtributoValores_ListAll(int obj_id, int? atr_id = null, int? ord_id = null)
        {
            try
            {
                List<ObjAtributoValores> lst = new List<ObjAtributoValores>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETO_ATRIBUTOS_VALORES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandTimeout = 600; // (tempo em segundos)
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@atr_id", atr_id);
                    com.Parameters.AddWithValue("@ord_id", ord_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new ObjAtributoValores
                        {
                            obj_id = (rdr["obj_id"] == DBNull.Value) ? -1 : Convert.ToInt32(rdr["obj_id"]),
                            atr_id = Convert.ToInt32(rdr["atr_id"]),
                            nItens = Convert.ToInt32(rdr["nItens"]),

                            atr_atributo_nome = rdr["atr_atributo_nome"].ToString(),
                            atr_descricao = rdr["atr_descricao"].ToString(),
                            atr_mascara_texto = (rdr["atr_mascara_texto"] == DBNull.Value) ? string.Empty : rdr["atr_mascara_texto"].ToString(),
                            atv_controle = rdr["atv_controle"].ToString(),

                            ati_ids = (rdr["ati_ids"] == DBNull.Value) ? "" : rdr["ati_ids"].ToString(),
                            atv_valor = (rdr["atv_valor"] == DBNull.Value) ? string.Empty : rdr["atv_valor"].ToString(),
                            atv_valores = (rdr["atv_valores"] == DBNull.Value) ? string.Empty : rdr["atv_valores"].ToString(),
                            atr_apresentacao_itens = (rdr["atr_apresentacao_itens"] == DBNull.Value) ? string.Empty : rdr["atr_apresentacao_itens"].ToString(),
                           // ati_item = (rdr["ati_item"] == DBNull.Value) ? string.Empty : rdr["ati_item"].ToString(),
                            atr_itens_todos =  rdr["atr_itens_todos"].ToString()
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
        ///  Salva os valores do ATRIBUTO no Banco
        /// </summary>
        /// <param name="ObjAtributoValor">Valor do Atributo</param>
        /// <param name="codigoOAE">Código ou Parte da Obra de Artea se localizar</param>
        /// <param name="selidTipoOAE">Id do Tipo de Obra de Arte a se filtrar</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param>
        /// <returns>int</returns>
        public int ObjAtributoValores_Salvar(ObjAtributoValores ObjAtributoValor, string codigoOAE, int selidTipoOAE, int usu_id, string ip, int ord_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "STP_UPD_OBJETO_ATRIBUTO_VALORES";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;
                    com.Parameters.AddWithValue("@obj_id", ObjAtributoValor.obj_id);

                    if (ObjAtributoValor.atr_id != -1)
                        com.Parameters.AddWithValue("@atr_id", ObjAtributoValor.atr_id);

                    if (ObjAtributoValor.ati_id != "-1" )
                        com.Parameters.AddWithValue("@ati_id", ObjAtributoValor.ati_id);

                        com.Parameters.AddWithValue("@nome_aba", ObjAtributoValor.nome_aba);
                        com.Parameters.AddWithValue("@atv_valores", ObjAtributoValor.atv_valores);

                        com.Parameters.AddWithValue("@codigoOAE", codigoOAE);
                        com.Parameters.AddWithValue("@selidTipoOAE", selidTipoOAE);

                    if (ord_id > 0 )
                        com.Parameters.AddWithValue("@ord_id", ord_id);

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
        ///  Salva valor de somente 1 atributo no Banco
        /// </summary>
        /// <param name="ObjAtributoValor">Valor do Atributo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjAtributoValor_Salvar(ObjAtributoValores ObjAtributoValor, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "STP_UPD_OBJ_ATRIBUTO_VALOR";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 3000000;
                    com.CommandTimeout = 600; // (tempo em segundos)
                    com.Parameters.AddWithValue("@obj_id", ObjAtributoValor.obj_id);
                    com.Parameters.AddWithValue("@atr_id", ObjAtributoValor.atr_id);

                    if (ObjAtributoValor.ati_id != "-1" )
                        com.Parameters.AddWithValue("@ati_id", ObjAtributoValor.ati_id);

                    com.Parameters.AddWithValue("@atv_valor", ObjAtributoValor.atv_valores);

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



        // *************** GRUPOS / VARIÁVEIS / VALORES DE INSPEÇÃO  *************************************************************
        /// <summary>
        /// Lista Grupos/Variáveis do Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param>
        /// <returns>Lista de ObjAtributoValores</returns>
        public List<GruposVariaveisValores> GruposVariaveisValores_ListAll(int obj_id, int? ord_id = -1)
        {
            try
            {
                List<GruposVariaveisValores> lst = new List<GruposVariaveisValores>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETO_GRUPO_OBJETO_VARIAVEIS_VALORES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@ord_id", ord_id);

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
                            ogv_id = (rdr["ogv_id"] == DBNull.Value) ? -1 :  Convert.ToInt32(rdr["ogv_id"]),
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
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// DataSet dos Grupos/Variáveis do Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param>
        /// <returns>Dataset</returns>
        public System.Data.DataSet GruposVariaveisValores_ListAll_DS(int obj_id, int? ord_id = -1)
        {
            try
            {
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETO_GRUPO_OBJETO_VARIAVEIS_VALORES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@ord_id", ord_id);

                    adapter.SelectCommand = com;
                    adapter.Fill(ds);

                    return ds;
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
        ///    Cria/Associa Grupos ao Objeto selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <param name="grupos_codigos">Codigos dos grupos a serem salvos</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int AssociarGruposVariaveis(int obj_id, string grupos_codigos, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_GRUPOS_VARIAVEIS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@obj_id", obj_id);
                    com.Parameters.AddWithValue("@grupos_codigos", grupos_codigos);
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
        ///  Insere ou Altera os dados das Variaveis do Grupo
        /// </summary>
        /// <param name="obj_id_tipoOAE">Id do objeto Tipo OAE</param>
        /// <param name="Ponto1">Espessura do Pavimento Ponto1</param>
        /// <param name="Ponto2">Espessura do Pavimento Ponto2</param>
        /// <param name="Ponto3">Espessura do Pavimento Ponto3</param>
        /// <param name="OutrasInformacoes">Outras Informações</param>
        /// <param name="listaConcatenada">Dados das Variaveis do Grupo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param>
        /// <returns>int</returns>
        public int GruposVariaveisValores_Salvar(int obj_id_tipoOAE, string Ponto1, string Ponto2, string Ponto3, string OutrasInformacoes, string listaConcatenada, int usu_id, string ip, int? ord_id = -1)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    SqlCommand com = new SqlCommand();
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "STP_UPD_GRUPOS_VARIAVEIS_VALORES";
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@obj_id_tipoOAE", obj_id_tipoOAE);
                    com.Parameters.AddWithValue("@Ponto1", Ponto1);
                    com.Parameters.AddWithValue("@Ponto2", Ponto2);
                    com.Parameters.AddWithValue("@Ponto3", Ponto3);
                    com.Parameters.AddWithValue("@OutrasInformacoes", OutrasInformacoes);
                    com.Parameters.AddWithValue("@listaConcatenada", listaConcatenada);

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
        /// Altera imagem do Esquema Estrutural
        /// </summary>
        /// <param name="EsquemaEstrutural">nome do Esquema Estrutural</param>
        /// <param name="usu_id_Atualizacao">Id do Usuário logado</param>
        /// <returns>int</returns>
        public int Usuario_AlterarFoto(string EsquemaEstrutural, int usu_id_Atualizacao)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            try
            {
                //string ImagePath = new ParametroBLL().Parametro_GetValor("ImagePath");

                conexao = new SqlConnection(strConn);
                cmd = new SqlCommand("STP_UPD_EsquemaEstrutural", conexao);

                cmd.Parameters.AddWithValue("@EsquemaEstrutural", EsquemaEstrutural);
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

        // *************** PRIORIZACAO  *************************************************************

        /// <summary>
        /// Lista de Objetos Priorizados
        /// </summary>
        /// <param name="CodRodovia">Filtro por Codigo da Rodovia</param>
        /// <param name="Regionais">Filtro por Regional</param>
        /// <param name="somenteINSP_ESPECIAIS">Filtro por Inspecao Especial</param>
        /// <returns>Lista de Objetos</returns>
        public List<ObjPriorizacao> ObjPriorizacao_ListAll(string CodRodovia, string Regionais, int? somenteINSP_ESPECIAIS = 0)
        {
            try
            {
                List<ObjPriorizacao> lst = new List<ObjPriorizacao>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETOS_PRIORIZACAO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@CodRodovia", CodRodovia);
                    com.Parameters.AddWithValue("@Regionais", Regionais);
                    com.Parameters.AddWithValue("@somenteINSP_ESPECIAIS", somenteINSP_ESPECIAIS);


                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new ObjPriorizacao
                        {
                            pri_id = Convert.ToInt32(rdr["pri_id"]),
                            ord_id = Convert.ToInt32(rdr["ord_id"]),
                            obj_id = Convert.ToInt32(rdr["obj_id"]),
                            obj_codigo = rdr["obj_codigo"].ToString(),
                            obj_descricao = rdr["obj_descricao"].ToString(),
                            pri_ordem = Convert.ToInt32(rdr["ordem"]),
                            pri_classificacao = (rdr["pri_classificacao"] == DBNull.Value) ? -1 : Convert.ToInt16(rdr["pri_classificacao"]),
                            pri_data_classificacao = rdr["pri_data_classificacao"].ToString(),
                            pri_data_inspecao = rdr["pri_data_inspecao"].ToString(),
                            pri_nota_final = (rdr["pri_nota_final"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_final"]), 2),
                            pri_nota_estrutura = (rdr["pri_nota_estrutura"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_estrutura"]), 2),
                            pri_nota_durabilidade = (rdr["pri_nota_durabilidade"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_durabilidade"]), 2),
                            pri_nota_acao = (rdr["pri_nota_acao"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_acao"]), 2),
                            pri_acao = rdr["pri_acao"].ToString(),

                            prs_id = rdr["prs_id"].ToString(),
                            pri_status = rdr["pri_status"].ToString(),
                            status_descricao = rdr["status_descricao"].ToString(),
                            corFundo = rdr["corFundo"].ToString(),

                            //pri_nota_funcionalidade = (rdr["pri_nota_funcionalidade"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_funcionalidade"]), 2),
                            pri_nota_importancia_oae_malha = (rdr["pri_nota_importancia_oae_malha"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_importancia_oae_malha"]), 2),
                            pri_nota_vdm = (rdr["pri_nota_vdm"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_vdm"]), 2),
                            pri_nota_principal_utilizacao = (rdr["pri_nota_principal_utilizacao"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_principal_utilizacao"]), 2),
                            pri_nota_facilidade_desvio = (rdr["pri_nota_facilidade_desvio"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_facilidade_desvio"]), 2),
                            pri_nota_gabarito_vertical = (rdr["pri_nota_gabarito_vertical"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_gabarito_vertical"]), 2),
                            pri_nota_gabarito_horizontal = (rdr["pri_nota_gabarito_horizontal"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_gabarito_horizontal"]), 2),
                            pri_nota_largura_plataforma = (rdr["pri_nota_largura_plataforma"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_largura_plataforma"]), 2),
                            pri_nota_agressividade_ambiental = (rdr["pri_nota_agressividade_ambiental"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_agressividade_ambiental"]), 2),
                            pri_nota_trem_tipo = (rdr["pri_nota_trem_tipo"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_trem_tipo"]), 2),
                            pri_nota_barreira_seguranca = (rdr["pri_nota_barreira_seguranca"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_nota_barreira_seguranca"]), 2),
                            pri_restricao_treminhoes = (rdr["pri_restricao_treminhoes"] == DBNull.Value) ? 0 : Math.Round(Convert.ToDouble(rdr["pri_restricao_treminhoes"]), 2),

                            ord_data_termino_execucao = (rdr["ord_data_termino_execucao"] == DBNull.Value) ? "" : rdr["ord_data_termino_execucao"].ToString(),
                            tos_descricao = (rdr["tos_descricao"] == DBNull.Value) ? "" :rdr["tos_descricao"].ToString()

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



    }
}