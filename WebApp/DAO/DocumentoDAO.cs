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
using System.Net;

namespace WebApp.DAO
{
    /// <summary>
    /// Documentos
    /// </summary>
    public class DocumentoDAO : Conexao
    {

        /// <summary>
        /// Lista de todos os Documentos não deletados
        /// </summary>
        /// <param name="doc_id">Filtro por Id de Documento, null para todos</param>
        /// <param name="doc_codigo">Filtro por Código de Documento, vazio para todos</param>
        /// <param name="doc_descricao">Filtro por Descrição de Documento, vazio para todos</param>
        /// <param name="tpd_id">Filtro por Tipo de Documento, vazio para todos</param>
        /// <param name="dcl_codigo">Filtro por Classe de Projeto de Documento, vazio para todos</param>
        /// <param name="usu_id">Id do usuário logado</param>
        /// <returns>Lista de Documentos</returns>
        public List<Documento> Documento_ListAll(int? doc_id,  string doc_codigo = "",  string doc_descricao = "",  string tpd_id = "",  string dcl_codigo = "", int? usu_id = null)
        {
            try
            {
                string CaminhoVirtualRaizArquivos = new ParametroDAO().Parametro_GetValor("CaminhoVirtualRaizArquivos");
                if (!CaminhoVirtualRaizArquivos.EndsWith("/"))
                    CaminhoVirtualRaizArquivos = CaminhoVirtualRaizArquivos + "/"; 

                List<Documento> lst = new List<Documento>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_DOCUMENTOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@doc_id", doc_id);
                    com.Parameters.AddWithValue("@doc_codigo", doc_codigo);
                    com.Parameters.AddWithValue("@doc_descricao", doc_descricao);
                    com.Parameters.AddWithValue("@tpd_id", tpd_id);
                    com.Parameters.AddWithValue("@dcl_codigo", dcl_codigo);
                    com.Parameters.AddWithValue("@usu_id", usu_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        string doc_caminho = rdr["doc_caminho"].ToString();
                        if (doc_caminho.Trim() != "")
                        {
                            doc_caminho = CaminhoVirtualRaizArquivos + doc_caminho;
                            doc_caminho = HttpUtility.HtmlDecode(doc_caminho);

                            if (!new Gerais().RemoteFileExists(doc_caminho))
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
                            dcl_id = (rdr["dcl_id"] == DBNull.Value) ? 0 : Convert.ToInt16(rdr["dcl_id"]),
                            dcl_codigo = (rdr["dcl_codigo"] == DBNull.Value) ? "" : rdr["dcl_codigo"].ToString(),
                            dcl_descricao = (rdr["dcl_descricao"] == DBNull.Value) ? "" : rdr["dcl_descricao"].ToString()
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
        /// Carrega o grid somente com a página solicitada
        /// </summary>
        /// <param name="doc_id">Filtro por Id de Documento, null para todos</param>
        /// <param name="doc_codigo">Filtro por Código de Documento, vazio para todos</param>
        /// <param name="doc_descricao">Filtro por Descrição de Documento, vazio para todos</param>
        /// <param name="tpd_id">Filtro por Tipo de Documento, vazio para todos</param>
        /// <param name="dcl_codigo">Filtro por Classe de Projeto de Documento, vazio para todos</param>
        /// <param name="start">Número do registro inícial da página</param>
        /// <param name="length">Quantidade de registros por página</param>
        /// <param name="Order_BY">Ordenado por</param>
        /// <param name="usu_id">Id do usuário logado</param>
        /// <returns>List do tipo Documento</returns>
        public List<Documento> LoadData(int? doc_id,  string doc_codigo = "",  string doc_descricao = "",  string tpd_id = "",  string dcl_codigo = "", int start = 0, int length =10 , string Order_BY= "", int? usu_id = null)
        {
            try
            {
                string CaminhoVirtualRaizArquivos = new ParametroDAO().Parametro_GetValor("CaminhoVirtualRaizArquivos");
                if (!CaminhoVirtualRaizArquivos.EndsWith("/"))
                    CaminhoVirtualRaizArquivos = CaminhoVirtualRaizArquivos + "/"; 

                List<Documento> lst = new List<Documento>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_DOCUMENTOS_2", con);
                    com.CommandType = CommandType.StoredProcedure;

                    com.CommandTimeout = 600; // (tempo em segundos)
                    com.Parameters.AddWithValue("@doc_id", doc_id);
                    com.Parameters.AddWithValue("@doc_codigo", doc_codigo);
                    com.Parameters.AddWithValue("@doc_descricao", doc_descricao);
                    com.Parameters.AddWithValue("@tpd_id", tpd_id);
                    com.Parameters.AddWithValue("@dcl_codigo", dcl_codigo);

                    com.Parameters.AddWithValue("@registro_ini", start);
                    com.Parameters.AddWithValue("@ordenado_por", Order_BY);
                    com.Parameters.AddWithValue("@qt_por_pagina", length);

                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        string doc_caminho = rdr["doc_caminho"].ToString();
                        if (doc_caminho.Trim() != "")
                        {
                            doc_caminho = CaminhoVirtualRaizArquivos + doc_caminho;
                            doc_caminho = HttpUtility.HtmlDecode(doc_caminho);

                            if (!new Gerais().RemoteFileExists(doc_caminho))
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
                            dcl_id = (rdr["dcl_id"] == DBNull.Value) ? 0 : Convert.ToInt16(rdr["dcl_id"]),
                            dcl_codigo = (rdr["dcl_codigo"] == DBNull.Value) ? "" : rdr["dcl_codigo"].ToString(),
                            dcl_descricao = (rdr["dcl_descricao"] == DBNull.Value) ? "" : rdr["dcl_descricao"].ToString(),
                            registro_ini = Convert.ToInt32(rdr["registro_ini"]),
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


        /// <summary>
        /// Insere ou Altera os dados do Documento 
        /// </summary>
        /// <param name="doc">Dados do Documento</param>
        /// <param name="usu_id">Id do usuário logado</param>
        /// <param name="ip">IP do usuário logado</param>
        /// <param name="doc_codigo_filtro">Dados do filtro por Codigo</param>
        /// <param name="doc_descricao_filtro">Dados do filtro por Descrição</param>
        /// <param name="tpd_id_filtro">Dados do filtro por Tipo</param>
        /// <param name="dcl_codigo_filtro">Dados do filtro por Classe</param>
        /// <param name="qt_por_pagina">Quantidade de registros por página</param>
        /// <param name="ordenado_por">Ordenado por</param>
        /// <returns>string</returns>
        public string Documento_Salvar(Documento doc, int usu_id, string ip,
            string doc_codigo_filtro = "", string doc_descricao_filtro = "", string tpd_id_filtro = "", string dcl_codigo_filtro = "",
            int qt_por_pagina = 10, string ordenado_por = "")
        {
            try
            {
                string CaminhoVirtualRaizArquivos = new ParametroDAO().Parametro_GetValor("CaminhoVirtualRaizArquivos");
                string doc_caminho = doc.doc_caminho.Replace(CaminhoVirtualRaizArquivos, "");
                doc_caminho = HttpUtility.HtmlEncode(doc_caminho);
                string saida = "0";
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (doc.doc_id > 0)
                        com.CommandText = "STP_UPD_DOCUMENTO";
                    else
                        com.CommandText = "STP_INS_DOCUMENTO";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    // System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    //p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    //com.Parameters.Add(p_return);
                    //com.Parameters[0].Size = 1000000; //int.MaxValue;

                    if (doc.doc_id > 0)
                        com.Parameters.AddWithValue("@doc_id", doc.doc_id);

                    com.Parameters.AddWithValue("@doc_codigo", doc.doc_codigo.ToUpper());
                    com.Parameters.AddWithValue("@doc_descricao", doc.doc_descricao);
                    com.Parameters.AddWithValue("@tpd_id", doc.tpd_id);
                    if (doc.dcl_id > 0)
                        com.Parameters.AddWithValue("@dcl_id ", doc.dcl_id);
                    com.Parameters.AddWithValue("@doc_caminho", doc_caminho);
                    com.Parameters.AddWithValue("@doc_ativo", doc.doc_ativo);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    com.Parameters.AddWithValue("@ip", ip);

                    if (doc.doc_id == 0) // se for insert, tem parametros adicionais
                    {
                        com.Parameters.AddWithValue("@doc_codigo_filtro", doc_codigo_filtro);
                        com.Parameters.AddWithValue("@doc_descricao_filtro", doc_descricao_filtro);
                        com.Parameters.AddWithValue("@tpd_id_filtro", tpd_id_filtro);
                        com.Parameters.AddWithValue("@dcl_codigo_filtro", dcl_codigo_filtro);
                        com.Parameters.AddWithValue("@ordenado_por", ordenado_por);
                        com.Parameters.AddWithValue("@qt_por_pagina", qt_por_pagina);
                    }

                 //   com.ExecuteScalar();

                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        saida = reader["saida"] == DBNull.Value ? "0" : reader["saida"].ToString();
                    }
                    //  return Convert.ToInt32(p_return.Value);



                      return saida;
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
        /// Ativa/Desativa Documento
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="usu_id">Id do usuário logado</param>
        /// <param name="ip">IP do usuário logado</param>
        /// <returns>int</returns>
        public int Documento_AtivarDesativar(int doc_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_DOCUMENTO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@doc_id", doc_id);
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
        /// Excluir (logicamente) Documento
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="usu_id">Id do usuário logado</param>
        /// <param name="ip">IP do usuário logado</param>
        /// <returns>int</returns>
        public int Documento_Excluir(int doc_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_DOCUMENTO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@doc_id", doc_id);
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


        // *************** TIPOS DE DOCUMENTO  *************************************************************

        /// <summary>
        /// Lista de todos Tipos de Documento 
        /// </summary>
        /// <param name="tpd_id">Filtro por Tipo de Documento, "" para todos</param>
        /// <param name="tpd_subtipo">Filtro por Subtipo de Documento, null para todos</param>
        /// <returns>Lista de DocTipo</returns>
        public List<DocTipo> DocTipo_ListAll(string tpd_id="", int? tpd_subtipo= null)
        {
            try
            {
                List<DocTipo> lst = new List<DocTipo>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_DOCUMENTO_TIPOS", con);
                    com.CommandType = CommandType.StoredProcedure;

                    if (tpd_id != "")
                        com.Parameters.AddWithValue("@tpd_id", tpd_id);

                    if (tpd_subtipo > 0)
                        com.Parameters.AddWithValue("@tpd_subtipo", tpd_subtipo);


                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new DocTipo
                        {   
                            tpd_id = rdr["tpd_id"].ToString(),
                            tpd_subtipo = Convert.ToInt32(rdr["tpd_subtipo"]),
                            tpd_descricao = rdr["tpd_descricao"].ToString()
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

        // *************** CLASSES DE DOCUMENTO  *************************************************************

        /// <summary>
        ///     Lista de todas as Classes de Documentos não deletadas
        /// </summary>
        /// <param name="dcl_id">Filtro por Id da Classe de Documento, null para todos</param>
        /// <returns>Lista da Classe de Documento</returns>
        public List<DocClasse> DocClasse_ListAll(int? dcl_id)
        {
            try
            {
                List<DocClasse> lst = new List<DocClasse>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_DOC_CLASSES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@dcl_id", dcl_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new DocClasse
                        {
                            dcl_id = Convert.ToInt32(rdr["dcl_id"]),
                            dcl_codigo = rdr["dcl_codigo"].ToString(),
                            dcl_descricao = rdr["dcl_descricao"].ToString(),
                            dcl_ativo = Convert.ToInt16(rdr["dcl_ativo"])
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
        ///    Insere ou Altera os dados da Classe de Documento
        /// </summary>
        /// <param name="dcl">Classe de Documento</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int DocClasse_Salvar(DocClasse dcl, int usu_id, string ip)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();
                    if (dcl.dcl_id > 0)
                        com.CommandText = "STP_UPD_DOC_CLASSE";
                    else
                        com.CommandText = "STP_INS_DOC_CLASSE";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    if (dcl.dcl_id > 0)
                        com.Parameters.AddWithValue("@dcl_id", dcl.dcl_id);

                    com.Parameters.AddWithValue("@dcl_codigo", dcl.dcl_codigo);
                    com.Parameters.AddWithValue("@dcl_descricao", dcl.dcl_descricao);
                    com.Parameters.AddWithValue("@dcl_ativo", dcl.dcl_ativo);
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
        ///     Excluir (logicamente) Classe de Documento
        /// </summary>
        /// <param name="dcl_id">Id da Classe do Documento Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int DocClasse_Excluir(int dcl_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_DEL_DOC_CLASSE", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@dcl_id", dcl_id);
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
        ///  Ativa/Desativa Classe de Documento
        /// </summary>
        /// <param name="dcl_id">Id da Classe do Documento Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int DocClasse_AtivarDesativar(int dcl_id, int usu_id, string ip)
        {
            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_ATIVARDESATIVAR_DOC_CLASSE", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@dcl_id", dcl_id);
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





        // *************** OBJETOS do Documento   *************************************************************
        /// <summary>
        /// Lista todos os OBJETOS do Documento selecionado
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <returns>Lista de Documento_Objeto</returns>
        public List<Documento_Objeto> Documento_Objetos_ListAll(int doc_id, int? usu_id = null)
        {

            try
            {
                List<Documento_Objeto> lst = new List<Documento_Objeto>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_DOCUMENTO_OBJETOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@doc_id", doc_id);
                    com.Parameters.AddWithValue("@usu_id", usu_id);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Documento_Objeto
                        {
                            doc_id =  doc_id,
                            obj_id = Convert.ToInt32(rdr["obj_id"]),
                            obj_codigo = rdr["obj_codigo"].ToString(),
                            obj_descricao = rdr["obj_descricao"].ToString(),
                            obj_Associado = Convert.ToInt16(rdr["obj_Associado"])
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
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="codObj">Codigo ou parte do Objeto a procurar</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <returns>Lista de Objetos Nao Associados</returns>
        public List<Objeto> Documento_ObjetosNaoAssociados_ListAll(int doc_id, string codObj, int? usu_id = null)
        {
            try
            {
                List<Objeto> lst = new List<Objeto>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_DOCUMENTO_OBJETOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    com.Parameters.AddWithValue("@doc_id", doc_id);
                    com.Parameters.AddWithValue("@obj_codigo", codObj);
                    com.Parameters.AddWithValue("@usu_id", usu_id);
                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Objeto
                        {
                            obj_id = Convert.ToInt32(rdr["obj_id"]),
                            obj_codigo = rdr["obj_codigo"].ToString(),
                            obj_descricao = rdr["obj_descricao"].ToString(),
                            obj_ativo = Convert.ToInt16(rdr["obj_ativo"])
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
        ///    Associa Documento aos Objetos selecionados
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="obj_ids">Ids dos Objetos Selecionados</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Documento_AssociarObjetos(int doc_id, string obj_ids, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_DOC_ASSOCIAR_OBJETOS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@doc_id", doc_id);
                    com.Parameters.AddWithValue("@obj_ids", obj_ids);
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
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="obj_id">Ids do Objeto Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Documento_DesassociarObjeto(int doc_id, int obj_id, int usu_id, string ip)
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


        // *************** ORDENS DE SERVICO do Documento   *************************************************************
       
        /// <summary>
        /// Lista todas as  ORDENS DE SERVICO do Documento selecionado
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <returns>Lista de Documento_Objeto</returns>
        public List<Documento_OrdemServico> Documento_OrdemServico_ListAll(int doc_id)
        {

            try
            {
                List<Documento_OrdemServico> lst = new List<Documento_OrdemServico>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_DOCUMENTO_OSS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@doc_id", doc_id);

                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        lst.Add(new Documento_OrdemServico
                        {
                            doc_id =  doc_id,
                            ord_id = Convert.ToInt32(rdr["ord_id"]),
                            ord_codigo = rdr["ord_codigo"].ToString(),
                            ord_descricao = rdr["ord_descricao"].ToString(),
                            ord_Associada = Convert.ToInt16(rdr["ord_Associada"])
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
        ///     Lista de todas as OSs não associadas para o Documento Selecionado
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="filtroOrdemServico_codigo">Codigo ou parte da OS a procurar</param>
        /// <param name="filtroObj_codigo">Codigo ou parte do Objeto OS a procurar</param>
        /// <param name="filtroTiposOS">Id do tipo de OS a procurar</param>
        /// <returns>Lista de OrdemServico Nao Associadas</returns>
        public List<OrdemServico> Documento_OrdemServicoNaoAssociadas_ListAll(int doc_id, string filtroOrdemServico_codigo, string filtroObj_codigo, int filtroTiposOS)
        {
            try
            {
                List<OrdemServico> lst = new List<OrdemServico>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_DOCUMENTO_OSS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    com.Parameters.AddWithValue("@doc_id", doc_id);

                    if (filtroOrdemServico_codigo.Trim() != "")
                        com.Parameters.AddWithValue("@filtroOrdemServico_codigo", filtroOrdemServico_codigo);

                    if (filtroObj_codigo.Trim() != "")
                        com.Parameters.AddWithValue("@filtroObj_codigo", filtroObj_codigo);

                    if (filtroTiposOS >= 0)
                        com.Parameters.AddWithValue("@filtroTiposOS", filtroTiposOS);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new OrdemServico
                        {
                            ord_id = Convert.ToInt32(rdr["ord_id"]),
                            ord_codigo = rdr["ord_codigo"].ToString(),
                            ord_descricao = rdr["ord_descricao"].ToString(),
                            ord_ativo = Convert.ToInt16(rdr["ord_ativo"])
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
        ///    Associa Documento às OSs selecionadas
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="ord_ids">Ids das OSs Selecionadas</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Documento_AssociarOrdemServico(int doc_id, string ord_ids, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_DOC_ASSOCIAR_OSs", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@doc_id", doc_id);
                    com.Parameters.AddWithValue("@ord_ids", ord_ids);
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
        ///    Desassocia Documento da OS selecionada
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="ord_id">Id da OS Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int Documento_DesassociarOrdemServico(int doc_id, int ord_id, int usu_id, string ip)
        {

            try
            {
                int i;
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_UPD_DOC_DESASSOCIAR_OS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@doc_id", doc_id);
                    com.Parameters.AddWithValue("@ord_id", ord_id);
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








    }

}