using System;
using System.Collections.Generic;
using WebApp.Models;
using System.Net.Http;
using System.Text;
using WebApp.Helpers;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace WebApp.DAO
{
    /// <summary>
    /// Integração
    /// </summary>
    public class IntegracaoDAO : Conexao
    {
        CultureInfo culturePTBR = new CultureInfo("pt-BR");

        /// <summary>
        /// Sincroniza os dados lidos do Sirgeo nas tabelas offline do Sigoa
        /// </summary>
        /// <returns></returns>
        public List<Rodovia> Sirgeo_SincronizarRodovias(int forcar_atualizacao = 0)
        {
            // busca pela API
            List<Rodovia> lstRodovias = get_Rodovias_API("");

            if (lstRodovias.Count == 0)
            {
                List<Rodovia> saida_erro = new List<Rodovia>();
                Rodovia err = new Rodovia();
                err.rod_codigo = "-1";
                err.rod_descricao = "Lista de Rodovias não encontrada no Sirgeo";
                saida_erro.Add(err);
                return saida_erro;

            }
            if (lstRodovias.Count == 1)
            {
                if (lstRodovias[0].rod_codigo == "-1")
                {
                    return lstRodovias;
                }
            }

            // transforma em DataTable
            DataTable dtRodovias = new Gerais().ToDataTable<Rodovia>(lstRodovias);

            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "Sirgeo.STP_UPD_SIRGEO_RODOVIAS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.Clear();
                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@forcar_atualizacao", forcar_atualizacao);

                    SqlParameter tvpParam = com.Parameters.AddWithValue("@rodovias", dtRodovias);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    com.ExecuteScalar();
                    if (p_return.Value.ToString() == "1")
                        return lstRodovias;
                    else
                    {
                        lstRodovias.Clear();
                        Rodovia err = new Rodovia();
                        err.rod_codigo = "-1";
                        err.rod_descricao = "Erro ao sincronizar Rodovias";

                        lstRodovias.Add(err);
                    }

                    return lstRodovias;
                }
            }
            catch (Exception ex)
            {
                List<Rodovia> saida_erro = new List<Rodovia>();
                Rodovia err = new Rodovia();
                err.rod_codigo = "-1";
                err.rod_descricao = ex.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
        }

        /// <summary>
        ///  Busca a lista de Rodovias pela API
        /// </summary>
        /// <param name="rod_Codigo">Codigo da Rodovia, vazio para todos</param>
        /// <returns>List Rodovia</returns>
        public List<Rodovia> get_Rodovias(string rod_Codigo = "")
        {
            try
            {
                bool sincronizar = true;
                 List<Rodovia> lst = new List<Rodovia>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("SIRGEO.STP_SEL_SIRGEO_RODOVIAS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@rod_Codigo", rod_Codigo);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Rodovia
                        {
                            rod_id = Convert.ToInt32(rdr["rod_id"]),
                            rod_codigo = rdr["rod_codigo"].ToString(),
                            rod_descricao = rdr["rod_descricao"].ToString(),
                            rod_km_inicial = rdr["rod_km_inicial"].ToString(),
                            rod_km_final = rdr["rod_km_final"].ToString(),
                            rod_km_extensao = rdr["rod_km_extensao"].ToString(),
                            rod_data_atualizacao = rdr["rod_data_atualizacao"] == DBNull.Value ? "" : rdr["rod_data_atualizacao"].ToString()
                        });


                        // checa a data da ultima atualizacao. se nao for do dia atual, atualiza
                        if (lst[0].rod_data_atualizacao != "")
                        {
                            if ((Convert.ToDateTime(lst[0].rod_data_atualizacao).Day == (DateTime.Today.Day)) &&
                                   (Convert.ToDateTime(lst[0].rod_data_atualizacao).Month == (DateTime.Today.Month)) &&
                                     (Convert.ToDateTime(lst[0].rod_data_atualizacao).Year == (DateTime.Today.Year)))
                            {
                                sincronizar = false;
                                continue;
                            }
                            else
                            {
                                sincronizar = true;
                                break;
                            }
                        }
                        else
                        {
                            sincronizar = true;
                            break;
                        }
                    }

                    if (sincronizar)
                    {
                        return Sirgeo_SincronizarRodovias(1);
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
        ///  Busca a lista de Rodovias pela API
        /// </summary>
        /// <param name="rod_Codigo">Codigo da Rodovia, vazio para todos</param>
        /// <returns>List Rodovia</returns>
        public List<Rodovia> get_Rodovias_API(string rod_Codigo = "")
        {
            string usu_id = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");
            string urlPath = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");

            try
            {
                string SIRGeo_URL = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");
                string SIRGeo_Usuario = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");


                using (var client = new HttpClient())
                {
                    var contentString = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                       {
                                                                            new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                       });

                    client.BaseAddress = new Uri(urlPath + "/Token");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(new Uri(urlPath + "/Token?usu_id=" + usu_id.ToString()), contentString).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        token token_retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<token>(responseBody);

                        if (token_retorno.tok_valido)
                        {
                            string nToken_Atual = token_retorno.tok_token;

                            using (var client2 = new HttpClient())
                            {
                                var contentString2 = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                                   {
                                                                                        new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                                   });
                                client2.BaseAddress = new Uri(urlPath + "/Rodovias");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                if (rod_Codigo.Trim() != "")
                                    rod_Codigo = "&rod_Codigo=" + rod_Codigo;
                                HttpResponseMessage response2 = client2.PostAsync(new Uri(urlPath + "/Rodovias?usu_id=" + usu_id.ToString() + "&token=" + nToken_Atual + rod_Codigo), contentString2).Result;

                                if (response2.IsSuccessStatusCode)
                                {
                                    var responseBody2 = response2.Content.ReadAsStringAsync().Result;

                                    // ajuste para deserializar a variavel em lista
                                    responseBody2 = "{Rodovias:" + responseBody2 + "}";

                                    lstRodovias listaDeRodovias = Newtonsoft.Json.JsonConvert.DeserializeObject<lstRodovias>(responseBody2);

                                    return listaDeRodovias.Rodovias;
                                }
                                else
                                {
                                    List<Rodovia> saida_erro = new List<Rodovia>();
                                    Rodovia err = new Rodovia();
                                    err.rod_codigo = "-1";
                                    err.rod_descricao = response2.ReasonPhrase;

                                    saida_erro.Add(err);
                                    return saida_erro;
                                }
                            }

                        }
                        else
                        {
                            List<Rodovia> saida_erro = new List<Rodovia>();
                            Rodovia err = new Rodovia();
                            err.rod_id = -1;
                            err.rod_codigo = "-1";
                            err.rod_descricao = "Token Inválido";

                            saida_erro.Add(err);
                            return saida_erro;
                        }

                    }
                    else
                    {
                        List<Rodovia> saida_erro = new List<Rodovia>();
                        Rodovia err = new Rodovia();
                        err.rod_id = -1;
                        err.rod_codigo = "-1";
                        err.rod_descricao = response.ReasonPhrase;

                        saida_erro.Add(err);
                        return saida_erro;

                    }

                }
            }
            catch (Exception ex)
            {
                List<Rodovia> saida_erro = new List<Rodovia>();
                Rodovia err = new Rodovia();
                err.rod_id = -1;
                err.rod_codigo = "-1";
                err.rod_descricao = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
            return null;
        }


        /// <summary>
        /// Sincroniza os dados lidos do Sirgeo nas tabelas offline do Sigoa
        /// </summary>
        /// <returns></returns>
        public List<OAE> Sirgeo_SincronizarOAE(int forcar_atualizacao = 0)
        {
            // busca pela API
            List<OAE> lstOAE = get_OAEs_API("");

            if (lstOAE.Count == 1)
            {
                if (lstOAE[0].rod_id == -1)
                {
                    return lstOAE;
                }
            }

            // transforma em DataTable
            DataTable dtOAE = new Gerais().ToDataTable<OAE>(lstOAE);

           
            DataView view = new DataView(dtOAE);
            DataTable distinctValues = view.ToTable(true, "rod_id", "oae_km_inicial", "oae_km_final", "sen_id", "reg_id", "oae_data_levantamento", "oae_extensao", "oat_id", "oae_data_criacao", "oae_id_sigoa", "oae_data_atualizacao");

            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "Sirgeo.STP_UPD_SIRGEO_OAEs";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.Clear();
                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@forcar_atualizacao", forcar_atualizacao);

                    SqlParameter tvpParam = com.Parameters.AddWithValue("@OAEs", distinctValues);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    com.ExecuteScalar();
                    if (p_return.Value.ToString() == "1")
                        return lstOAE;
                    else
                    {
                        lstOAE.Clear();
                        OAE err = new OAE();
                        err.rod_id = -1;
                        err.oae_km_inicial =  "Erro ao sincronizar OAEs";

                        lstOAE.Add(err);
                    }

                    return lstOAE;
                }
            }
            catch (Exception ex)
            {
                List<OAE> saida_erro = new List<OAE>();
                OAE err = new OAE();
                err.rod_id = -1;
                err.oae_km_inicial = ex.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
        }

        /// <summary>
        ///  Busca a lista de OAEs pela API
        /// </summary>
        /// <param name="rod_id">ID da Rodovia, vazio para todos</param>
        /// <returns>List Rodovia</returns>
        public List<OAE> get_OAEs_API(string rod_id)
        {
            string usu_id = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");
            string urlPath = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");

            try
            {
                string SIRGeo_URL = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");
                string SIRGeo_Usuario = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");


                using (var client = new HttpClient())
                {
                    var contentString = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                       {
                                                                            new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                       });

                    client.BaseAddress = new Uri(urlPath + "/Token");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(new Uri(urlPath + "/Token?usu_id=" + usu_id.ToString()), contentString).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        token token_retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<token>(responseBody);

                        if (token_retorno.tok_valido)
                        {
                            string nToken_Atual = token_retorno.tok_token;

                            using (var client2 = new HttpClient())
                            {
                                var contentString2 = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                                   {
                                                                                        new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                                   });
                                client2.BaseAddress = new Uri(urlPath + "/OaeLevantamento");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                if ((rod_id.Trim() != "") && (new Gerais().IsNumeric(rod_id)))
                                    rod_id = "&rod_id=" + rod_id.ToString();
                                HttpResponseMessage response2 = client2.PostAsync(new Uri(urlPath + "/OaeLevantamento?usu_id=" + usu_id.ToString() + "&token=" + nToken_Atual + rod_id), contentString2).Result;

                                if (response2.IsSuccessStatusCode)
                                {
                                    var responseBody2 = response2.Content.ReadAsStringAsync().Result;

                                    // ajuste para deserializar a variavel em lista
                                    responseBody2 = "{OAEs:" + responseBody2 + "}";

                                    lstOAEs listaDeOAEs = Newtonsoft.Json.JsonConvert.DeserializeObject<lstOAEs>(responseBody2);

                                    return listaDeOAEs.OAEs;
                                }
                                else
                                {
                                    List<OAE> saida_erro = new List<OAE>();
                                    OAE err = new OAE();
                                    err.rod_id = -1;
                                    err.oae_km_inicial = response2.ReasonPhrase;

                                    saida_erro.Add(err);
                                    return saida_erro;

                                }

                            }

                        }
                        else
                        {
                            List<OAE> saida_erro = new List<OAE>();
                            OAE err = new OAE();
                            err.rod_id = -1;
                            err.oae_km_inicial = "Token Inválido";

                            saida_erro.Add(err);
                            return saida_erro;
                        }

                    }
                    else
                    {
                        List<OAE> saida_erro = new List<OAE>();
                        OAE err = new OAE();
                        err.rod_id = -1;
                        err.oae_km_inicial = response.ReasonPhrase;

                        saida_erro.Add(err);
                        return saida_erro;

                    }
                }
            }
            catch (Exception ex)
            {
                List<OAE> saida_erro = new List<OAE>();
                OAE err = new OAE();
                err.rod_id = -1;
                err.oae_km_inicial = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
            return null;
        }


        /// <summary>
        ///  Busca a lista de OAEs
        /// </summary>
        /// <returns>List OAEs</returns>
        public List<OAE> get_OAEs()
        {
            try
            {
                bool sincronizar = true;
                List<OAE> lst = new List<OAE>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("SIRGEO.STP_SEL_SIRGEO_OAEs", con);
                    com.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new OAE
                        {
                            rod_id = Convert.ToInt32(rdr["rod_id"]),
                            oae_km_inicial = rdr["oae_km_inicial"].ToString(),
                            oae_km_final = rdr["oae_km_final"].ToString(),
                            sen_id = Convert.ToInt32(rdr["sen_id"]),
                            reg_id = Convert.ToInt32(rdr["reg_id"]),
                            oae_data_levantamento = rdr["oae_data_levantamento"].ToString(),
                            oae_extensao = rdr["oae_extensao"].ToString(),
                            oat_id = rdr["oat_id"].ToString(),
                            oae_data_criacao = rdr["oae_data_criacao"].ToString(),
                            oae_id_sigoa = rdr["oae_id_sigoa"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["oae_id_sigoa"]),
                            oae_data_atualizacao = rdr["oae_data_atualizacao"] == DBNull.Value ? "" : rdr["oae_data_atualizacao"].ToString()
                        });

                        // checa a data da ultima atualizacao. se nao for do dia atual, atualiza
                        if (lst[0].oae_data_atualizacao != "")
                        {
                            if ((Convert.ToDateTime(lst[0].oae_data_atualizacao).Day == (DateTime.Today.Day)) &&
                                   (Convert.ToDateTime(lst[0].oae_data_atualizacao).Month == (DateTime.Today.Month)) &&
                                     (Convert.ToDateTime(lst[0].oae_data_atualizacao).Year == (DateTime.Today.Year)))
                            {
                                sincronizar = false;
                                continue;
                            }
                            else
                            {
                                sincronizar = true;
                                break;
                            }
                        }
                        else
                        {
                            sincronizar = true;
                            break;
                        }
                    }

                    if (sincronizar)
                    {
                        return Sirgeo_SincronizarOAE(1);
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
        /// Sincroniza os dados lidos do Sirgeo nas tabelas offline do Sigoa
        /// </summary>
        /// <returns></returns>
        public List<Regional> Sirgeo_SincronizarRegionais(int forcar_atualizacao = 0)
        {
            // busca pela API
            List<Regional> lstRegionais = get_Regionais_API();

            if (lstRegionais.Count == 1)
            {
                if (lstRegionais[0].reg_id == -1)
                {
                    return lstRegionais;
                }
            }


            // transforma em DataTable
            DataTable dtRegionais = new Gerais().ToDataTable<Regional>(lstRegionais);

            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "Sirgeo.STP_UPD_SIRGEO_REGIONAIS";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.Clear();
                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@forcar_atualizacao", forcar_atualizacao);

                    SqlParameter tvpParam = com.Parameters.AddWithValue("@regionais", dtRegionais);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    com.ExecuteScalar();
                    if (p_return.Value.ToString() == "1")
                        return lstRegionais;
                    else
                    {
                        lstRegionais.Clear();
                        Regional err = new Regional();
                        err.reg_codigo = "-1";
                        err.reg_descricao = "Erro ao sincronizar Regionais";

                        lstRegionais.Add(err);
                    }

                    return lstRegionais;
                }
            }
            catch (Exception ex)
            {
                List<Regional> saida_erro = new List<Regional>();
                Regional err = new Regional();
                err.reg_codigo = "-1";
                err.reg_descricao = ex.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
        }

        /// <summary>
        ///  Busca a lista de Regionais
        /// </summary>
        /// <returns>List Regionais</returns>
        public List<Regional> get_Regionais()
        {
            try
            {
                bool sincronizar = true;
                List<Regional> lst = new List<Regional>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("SIRGEO.STP_SEL_SIRGEO_REGIONAIS", con);
                    com.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new Regional
                        {
                            reg_id = Convert.ToInt32(rdr["reg_id"]),
                            reg_codigo = rdr["reg_codigo"].ToString(),
                            reg_descricao = rdr["reg_descricao"].ToString(),
                            reg_logradouro = rdr["reg_logradouro"].ToString(),
                            reg_bairro = rdr["reg_bairro"].ToString(),
                            reg_cep = rdr["reg_cep"].ToString(),
                            reg_email = rdr["reg_email"].ToString(),
                            reg_ddd_telefone = rdr["reg_ddd_telefone"].ToString(),
                            reg_telefone = rdr["reg_telefone"].ToString(),
                            reg_ddd_telefone_cco = rdr["reg_ddd_telefone_cco"].ToString(),
                            reg_telefone_cco = rdr["reg_telefone_cco"].ToString(),
                            reg_ddd_fax = rdr["reg_ddd_fax"].ToString(),
                            reg_fax = rdr["reg_fax"].ToString(),
                            reg_data_atualizacao = rdr["reg_data_atualizacao"] == DBNull.Value ? "" : rdr["reg_data_atualizacao"].ToString()
                        });


                        // checa a data da ultima atualizacao. se nao for do dia atual, atualiza
                        if (lst[0].reg_data_atualizacao != "")
                        {
                            if ((Convert.ToDateTime(lst[0].reg_data_atualizacao).Day == (DateTime.Today.Day)) &&
                                   (Convert.ToDateTime(lst[0].reg_data_atualizacao).Month == (DateTime.Today.Month)) &&
                                     (Convert.ToDateTime(lst[0].reg_data_atualizacao).Year == (DateTime.Today.Year)))
                            {
                                sincronizar = false;
                                continue;
                            }
                            else
                            {
                                sincronizar = true;
                                break;
                            }
                        }
                        else
                        {
                            sincronizar = true;
                            break;
                        }
                    }

                    if (sincronizar)
                    {
                        return Sirgeo_SincronizarRegionais(1);
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
        ///  Busca a lista de Regionais pela API
        /// </summary>
        /// <returns>List Regional</returns>
        public List<Regional> get_Regionais_API()
        {
            string usu_id = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");
            string urlPath = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");

            try
            {
                string SIRGeo_URL = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");
                string SIRGeo_Usuario = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");


                using (var client = new HttpClient())
                {
                    var contentString = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                       {
                                                                            new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                       });

                    client.BaseAddress = new Uri(urlPath + "/Token");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(new Uri(urlPath + "/Token?usu_id=" + usu_id.ToString()), contentString).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        token token_retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<token>(responseBody);

                        if (token_retorno.tok_valido)
                        {
                            string nToken_Atual = token_retorno.tok_token;

                            using (var client2 = new HttpClient())
                            {
                                var contentString2 = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                                   {
                                                                                        new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                                   });
                                client2.BaseAddress = new Uri(urlPath + "/Dominio");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                HttpResponseMessage response2 = client2.PostAsync(new Uri(urlPath + "/Dominio?dominio=regionais&usu_id=" + usu_id.ToString() + "&token=" + nToken_Atual), contentString2).Result;

                                if (response2.IsSuccessStatusCode)
                                {
                                    var responseBody2 = response2.Content.ReadAsStringAsync().Result;

                                    // ajuste para deserializar a variavel em lista
                                    responseBody2 = "{Regionais:" + responseBody2 + "}";

                                    lstRegionais listaDeRegionais = Newtonsoft.Json.JsonConvert.DeserializeObject<lstRegionais>(responseBody2);

                                    return listaDeRegionais.Regionais;
                                }
                                else
                                {
                                    List<Regional> saida_erro = new List<Regional>();
                                    Regional err = new Regional();
                                    err.reg_id = -1;
                                    err.reg_codigo = response2.ReasonPhrase;

                                    saida_erro.Add(err);
                                    return saida_erro;

                                }
                            }

                        }
                        else
                        {
                            List<Regional> saida_erro = new List<Regional>();
                            Regional err = new Regional();
                            err.reg_id = -1;
                            err.reg_codigo = "Token Inválido";

                            saida_erro.Add(err);
                            return saida_erro;
                        }

                    }
                    else
                    {
                        List<Regional> saida_erro = new List<Regional>();
                        Regional err = new Regional();
                        err.reg_id = -1;
                        err.reg_codigo = response.ReasonPhrase;

                        saida_erro.Add(err);
                        return saida_erro;

                    }

                }
            }
            catch (Exception ex)
            {
                List<Regional> saida_erro = new List<Regional>();
                Regional err = new Regional();
                err.reg_id = -1;
                err.reg_codigo = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
            return null;
        }


        /// <summary>
        /// Concatena a lista de Regionais obtidas no Sirgeo
        /// </summary>
        /// <returns>string</returns>
        public string str_Regionais()
        {
            List<Regional> lstRegionais = get_Regionais();
            string strRegionais = "";

            for (int i = 0; i < lstRegionais.Count; i++)
                strRegionais = strRegionais + ";" + lstRegionais[i].reg_id.ToString() + ":" + lstRegionais[i].reg_codigo + "|" + lstRegionais[i].reg_descricao;

            // remove o 1o ponto e virgula
            strRegionais = strRegionais.Substring(1);

            return strRegionais;
        }


        /// <summary>
        ///  Busca a lista de SentidoRodovias pela API
        /// </summary>
        /// <returns>List SentidoRodovia</returns>
        public List<SentidoRodovia> get_SentidoRodovias()
        {
            string usu_id = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");
            string urlPath = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");

            try
            {
                string SIRGeo_URL = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");
                string SIRGeo_Usuario = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");


                using (var client = new HttpClient())
                {
                    var contentString = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                       {
                                                                            new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                       });

                    client.BaseAddress = new Uri(urlPath + "/Token");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(new Uri(urlPath + "/Token?usu_id=" + usu_id.ToString()), contentString).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        token token_retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<token>(responseBody);

                        if (token_retorno.tok_valido)
                        {
                            string nToken_Atual = token_retorno.tok_token;

                            using (var client2 = new HttpClient())
                            {
                                var contentString2 = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                                   {
                                                                                        new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                                   });
                                client2.BaseAddress = new Uri(urlPath + "/SentidoRodovias");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                HttpResponseMessage response2 = client2.PostAsync(new Uri(urlPath + "/Dominio?dominio=rodovias_sentidos&usu_id=" + usu_id.ToString() + "&token=" + nToken_Atual), contentString2).Result;

                                if (response2.IsSuccessStatusCode)
                                {
                                    var responseBody2 = response2.Content.ReadAsStringAsync().Result;

                                    // ajuste para deserializar a variavel em lista
                                    responseBody2 = "{SentidosRodovia:" + responseBody2 + "}";

                                    lstSentidoRodovia listaDeSentidoRodovias = Newtonsoft.Json.JsonConvert.DeserializeObject<lstSentidoRodovia>(responseBody2);

                                    return listaDeSentidoRodovias.SentidosRodovia;
                                }
                            }

                        }
                    }
                    else
                    {
                        List<SentidoRodovia> saida_erro = new List<SentidoRodovia>();
                        SentidoRodovia err = new SentidoRodovia();
                        err.sen_id = -1;
                        err.sen_descricao = response.ReasonPhrase;

                        saida_erro.Add(err);
                        return saida_erro;

                    }

                }
            }
            catch (Exception ex)
            {
                List<SentidoRodovia> saida_erro = new List<SentidoRodovia>();
                SentidoRodovia err = new SentidoRodovia();
                err.sen_id = -1;
                err.sen_descricao = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
            return null;
        }


        /// <summary>
        ///  Busca a lista de TipoOAE pela API
        /// </summary>
        /// <returns>List TipoOAE</returns>
        public List<TipoOAE> get_TiposOAE()
        {
            string usu_id = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");
            string urlPath = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");

            try
            {
                string SIRGeo_URL = new ParametroDAO().Parametro_GetValor("SIRGeo_URL");
                string SIRGeo_Usuario = new ParametroDAO().Parametro_GetValor("SIRGeo_Usuario");


                using (var client = new HttpClient())
                {
                    var contentString = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                       {
                                                                            new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                       });

                    client.BaseAddress = new Uri(urlPath + "/Token");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(new Uri(urlPath + "/Token?usu_id=" + usu_id.ToString()), contentString).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        token token_retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<token>(responseBody);

                        if (token_retorno.tok_valido)
                        {
                            string nToken_Atual = token_retorno.tok_token;

                            using (var client2 = new HttpClient())
                            {
                                var contentString2 = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                                                                                   {
                                                                                        new KeyValuePair<string, string>("usu_id", usu_id.ToString())
                                                                                   });
                                client2.BaseAddress = new Uri(urlPath + "/oae_tipos");
                                client2.DefaultRequestHeaders.Accept.Clear();
                                client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                                HttpResponseMessage response2 = client2.PostAsync(new Uri(urlPath + "/Dominio?dominio=oae_tipos&usu_id=" + usu_id.ToString() + "&token=" + nToken_Atual), contentString2).Result;

                                if (response2.IsSuccessStatusCode)
                                {
                                    var responseBody2 = response2.Content.ReadAsStringAsync().Result;

                                    // ajuste para deserializar a variavel em lista
                                    responseBody2 = "{TiposOAE:" + responseBody2 + "}";

                                    lstTipoOAE listaDeTipoOAEs = Newtonsoft.Json.JsonConvert.DeserializeObject<lstTipoOAE>(responseBody2);

                                    return listaDeTipoOAEs.TiposOAE;
                                }
                            }

                        }
                    }
                    else
                    {
                        List<TipoOAE> saida_erro = new List<TipoOAE>();
                        TipoOAE err = new TipoOAE();
                        err.oat_id = -1;
                        err.oat_descricao = response.ReasonPhrase;

                        saida_erro.Add(err);
                        return saida_erro;

                    }

                }
            }
            catch (Exception ex)
            {
                List<TipoOAE> saida_erro = new List<TipoOAE>();
                TipoOAE err = new TipoOAE();
                err.oat_id = -1;
                err.oat_descricao = ex.InnerException.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
            return null;
        }


        // ***************  APIs DER **************

        /// <summary>
        /// Sincroniza os dados lidos do Sirgeo nas tabelas offline do Sigoa
        /// </summary>
        /// <returns></returns>
        public List<vdm> DER_SincronizarVDMs(int forcar_atualizacao,string rod_codigo, decimal kminicial, decimal kmfinal)
        {
            // busca pela API
            List<vdm> lstVDMs = get_VDMs_API(rod_codigo, kminicial, kmfinal);

            if (lstVDMs.Count == 1)
            {
                if (lstVDMs[0].vdm_ano == -1)
                {
                    return lstVDMs;
                }
            }

            // transforma em DataTable
            DataTable dtVDMs = new Gerais().ToDataTable<vdm>(lstVDMs);

            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "DER.STP_UPD_DER_VDMs";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.Clear();
                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@forcar_atualizacao", forcar_atualizacao);
                    com.Parameters.AddWithValue("@rod_codigo", rod_codigo);
                    com.Parameters.AddWithValue("@kminicial", kminicial);
                    com.Parameters.AddWithValue("@kmfinal", kmfinal);

                    SqlParameter tvpParam = com.Parameters.AddWithValue("@VDMs", dtVDMs);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    com.ExecuteScalar();
                    if (p_return.Value.ToString() == "1")
                        return lstVDMs;
                    else
                    {
                        lstVDMs.Clear();
                        vdm err = new vdm();
                        err.vdm_ano = -1;
                        err.vdm_rodovia = "Erro ao sincronizar VDMs";

                        lstVDMs.Add(err);
                    }

                    return lstVDMs;
                }
            }
            catch (Exception ex)
            {
                List<vdm> saida_erro = new List<vdm>();
                vdm err = new vdm();
                err.vdm_ano = -1;
                err.vdm_rodovia = ex.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
        }

 
        /// <summary>
        /// Busca a lista de VDMs na API para a rodovia solicitada
        /// </summary>
        /// <param name="rod_codigo">Código da Rodovia</param>
        /// <param name="kminicial">Km inicial solicitado</param>
        /// <param name="kmfinal">Km final solicitado</param>
        /// <returns>Lista vdm</returns>
        public List<vdm> get_VDMs_API(string rod_codigo, decimal kminicial, decimal kmfinal)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    vdm_entrada VDMs_In = new vdm_entrada();
                    VDMs_In.modo = "consolidado";
                    VDMs_In.rodovia = rod_codigo.Replace(" ", "");
                    VDMs_In.kminicial = kminicial;
                    VDMs_In.kmfinal = kmfinal;

                    string DER_API_VDM_URL = new ParametroDAO().Parametro_GetValor("DER_API_VDM_URL");
                    string DER_API_VDM_Usuario = new ParametroDAO().Parametro_GetValor("DER_API_VDM_Usuario");
                    string DER_API_VDM_Senha = new ParametroDAO().Parametro_GetValor("DER_API_VDM_Senha");

                    var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(VDMs_In), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Auth", DER_API_VDM_Usuario + "-" + DER_API_VDM_Senha);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PostAsync(new Uri(DER_API_VDM_URL), stringContent).Result;
                    var content = response.Content;
                    var headers = response.Headers;
                    var status = response.StatusCode;

                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    vdm_retorno retorno2 = null;
                    retorno_erro error = null;

                    // verifica se tem erro
                    try
                    {
                        retorno2 = Newtonsoft.Json.JsonConvert.DeserializeObject<vdm_retorno>(responseBody);
                    }
                    catch
                    {
                        error = Newtonsoft.Json.JsonConvert.DeserializeObject<retorno_erro>(responseBody);
                    }

                    if (retorno2 != null)
                    {
                        if (retorno2.status)
                        {
                            // retorna Lista<vdm>
                            return retorno2.data.Vdms;
                        }
                    }
                    else
                    if (error != null)
                        if (!error.status)
                        {
                            List<vdm> saida_erro = new List<vdm>();
                            vdm err = new vdm();
                            err.vdm_ano = -1;
                            err.vdm_rodovia = error.data;

                            saida_erro.Add(err);
                            return saida_erro;
                        }
                }


            }
            catch (Exception ex)
            {
                List<vdm> saida_erro = new List<vdm>();
                vdm err = new vdm();
                err.vdm_ano = -1;
                err.vdm_rodovia = ex.Message;

                saida_erro.Add(err);
                return saida_erro;
            }

            return null;
        }


        /// <summary>
        ///  Busca a lista de VDMs
        /// </summary>
        /// <param name="rod_codigo">Código da Rodovia</param>
        /// <param name="kminicial">Km inicial solicitado</param>
        /// <param name="kmfinal">Km final solicitado</param>
        /// <returns>Lista vdm</returns>
        public List<vdm> get_VDMs(string rod_codigo, decimal kminicial, decimal kmfinal)
        {
            try
            {
                bool sincronizar = true;
                List<vdm> lst = new List<vdm>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("DER.STP_SEL_DER_VDMs", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@rod_codigo", rod_codigo);
                    com.Parameters.AddWithValue("@kminicial", kminicial);
                    com.Parameters.AddWithValue("@kmfinal", kmfinal);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new vdm
                        {
                            vdm_ano = rdr["vdm_ano"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["vdm_ano"]),
                            vdm_rodovia = rdr["vdm_rodovia"] == DBNull.Value ? "" : rdr["vdm_rodovia"].ToString(),
                            pcl_numero = rdr["pcl_numero"] == DBNull.Value ? -1 : Convert.ToInt32(rdr["pcl_numero"]),

                            pcl_kminicial = rdr["pcl_kminicial"] == DBNull.Value ? "" : rdr["pcl_kminicial"].ToString(),
                            pcl_kmfinal = rdr["pcl_kmfinal"] == DBNull.Value ? "" : rdr["pcl_kmfinal"].ToString(),

                            vdm_sentido1 = rdr["vdm_sentido1"] == DBNull.Value ? "" : rdr["vdm_sentido1"].ToString(),
                            vdm_passeio1 = rdr["vdm_passeio1"] == DBNull.Value ? "" : rdr["vdm_passeio1"].ToString(),
                            vdm_com1 = rdr["vdm_com1"] == DBNull.Value ? "" : rdr["vdm_com1"].ToString(),
                            vdm_moto1 = rdr["vdm_moto1"] == DBNull.Value ? "" : rdr["vdm_moto1"].ToString(),
                            vdm_valor1 = rdr["vdm_valor1"] == DBNull.Value ? "" : rdr["vdm_valor1"].ToString(),

                            vdm_sentido2 = rdr["vdm_sentido2"] == DBNull.Value ? "" : rdr["vdm_sentido2"].ToString(),
                            vdm_passeio2 = rdr["vdm_passeio2"] == DBNull.Value ? "" : rdr["vdm_passeio2"].ToString(),
                            vdm_com2 = rdr["vdm_com2"] == DBNull.Value ? "" : rdr["vdm_com2"].ToString(),
                            vdm_moto2 = rdr["vdm_moto2"] == DBNull.Value ? "" : rdr["vdm_moto2"].ToString(),
                            vdm_valor2 = rdr["vdm_valor2"] == DBNull.Value ? "" : rdr["vdm_valor2"].ToString(),

                            vdm_passeio_bidirecional = rdr["vdm_passeio_bidirecional"] == DBNull.Value ? "" : rdr["vdm_passeio_bidirecional"].ToString(),
                            vdm_com_bidirecional = rdr["vdm_com_bidirecional"] == DBNull.Value ? "" : rdr["vdm_com_bidirecional"].ToString(),
                            vdm_moto_bidirecional = rdr["vdm_moto_bidirecional"] == DBNull.Value ? "" : rdr["vdm_moto_bidirecional"].ToString(),
                            vdm_bidirecional = rdr["vdm_bidirecional"] == DBNull.Value ? "" : rdr["vdm_bidirecional"].ToString(),
                            vdm_data_atualizacao = rdr["vdm_data_atualizacao"] == DBNull.Value ? "" : rdr["vdm_data_atualizacao"].ToString()
                        });


                        // checa a data da ultima atualizacao. se nao for do dia atual, atualiza
                        if (lst[0].vdm_data_atualizacao != "")
                        {
                            if ((Convert.ToDateTime(lst[0].vdm_data_atualizacao).Day == (DateTime.Today.Day)) &&
                                   (Convert.ToDateTime(lst[0].vdm_data_atualizacao).Month == (DateTime.Today.Month)) &&
                                     (Convert.ToDateTime(lst[0].vdm_data_atualizacao).Year == (DateTime.Today.Year)))
                            {
                                sincronizar = false;
                                continue;
                            }
                            else
                            {
                                sincronizar = true;
                                break;
                            }
                        }
                        else
                        {
                            sincronizar = true;
                            break;
                        }
                    }

                    if (sincronizar)
                    {
                        return DER_SincronizarVDMs(1, rod_codigo, kminicial, kmfinal);
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
        /// Sincroniza os dados lidos do Sirgeo nas tabelas offline do Sigoa
        /// </summary>
        /// <returns></returns>
        public List<tpu> DER_SincronizarTPUs(int forcar_atualizacao, string ano, string fase, string mes, string onerado = "", string codSubItem = "")
        {
            // busca pela API
            List<tpu> lstTPUs = get_TPUs_API(ano, fase, mes, onerado, codSubItem);

            if (lstTPUs.Count == 1)
            {
                if (lstTPUs[0].DataTpu == "-1")
                {
                    return lstTPUs;
                }
            }


            // transforma em DataTable
            DataTable dtTPUs = new Gerais().ToDataTable<tpu>(lstTPUs);

            try
            {
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand();

                    com.CommandText = "DER.STP_UPD_DER_TPUs";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.Clear();
                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@forcar_atualizacao", forcar_atualizacao);
                    com.Parameters.AddWithValue("@ano", ano);
                    com.Parameters.AddWithValue("@mes", mes);
                    com.Parameters.AddWithValue("@fase", fase);

                    SqlParameter tvpParam = com.Parameters.AddWithValue("@TPUs", dtTPUs);
                    tvpParam.SqlDbType = SqlDbType.Structured;

                    com.ExecuteScalar();
                    if (p_return.Value.ToString() == "1")
                        return lstTPUs;
                    else
                    {
                        lstTPUs.Clear();
                        tpu err = new tpu();
                        err.CodSubItem = "-1";
                        err.NomeSubItem = "Erro ao sincronizar TPUs";

                        lstTPUs.Add(err);
                    }

                    return lstTPUs;
                }
            }
            catch (Exception ex)
            {
                List<tpu> saida_erro = new List<tpu>();
                tpu err = new tpu();
                err.DataTpu = "-1";
                err.CodSubItem = ex.Message;

                saida_erro.Add(err);
                return saida_erro;
            }
        }

        /// <summary>
        /// Lista das TPUs
        /// </summary>
        /// <param name="ano">Ano</param>
        /// <param name="fase">fase = 23</param>
        /// <param name="mes">Mês</param>
        /// <param name="onerado">Onerado: SIM,NÃO, vazio para todos</param>
        /// <param name="codSubItem">codSubItem: Opcional</param>
        /// <returns>Lista tpu</returns>
        public List<tpu> get_TPUs_API(string ano, string fase, string mes, string onerado = "", string codSubItem = "")
        {

            try
            {
                string DER_API_TPU_URL = new ParametroDAO().Parametro_GetValor("DER_API_TPU_URL");
                string DER_API_TPU_Usuario = new ParametroDAO().Parametro_GetValor("DER_API_TPU_Usuario");
                string DER_API_TPU_Senha = new ParametroDAO().Parametro_GetValor("DER_API_TPU_Senha");

                using (var client = new HttpClient())
                {

                    tpu_entrada tpus_In = new tpu_entrada();
                    tpus_In.modo = "TPU";
                    tpus_In.ano = ano;
                    tpus_In.fase = fase;
                    tpus_In.mes = mes;
                    tpus_In.onerado = onerado;
                    tpus_In.CodSubItem = codSubItem;


                    var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(tpus_In), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Auth", DER_API_TPU_Usuario + "-" + DER_API_TPU_Senha);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PostAsync(new Uri(DER_API_TPU_URL), stringContent).Result;
                    var content = response.Content;
                    var headers = response.Headers;
                    var status = response.StatusCode;

                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    tpu_retorno retorno2 = null;
                    retorno_erro error = null;

                    // verifica se tem erro
                    try
                    {
                        retorno2 = Newtonsoft.Json.JsonConvert.DeserializeObject<tpu_retorno>(responseBody);
                    }
                    catch
                    {
                        error = Newtonsoft.Json.JsonConvert.DeserializeObject<retorno_erro>(responseBody);
                    }

                    if (retorno2 != null)
                    {
                        if (retorno2.status)
                        {
                            List<tpu> retorno = retorno2.data.TpuPrecosSites;
                            return retorno;
                        }
                    }
                    else
                    if (error != null)
                        if (!error.status)
                        {
                            List<tpu> saida_erro = new List<tpu>();
                            tpu err = new tpu();
                            err.DataTpu = "-1";
                            err.CodSubItem = error.data;

                            saida_erro.Add(err);
                            return saida_erro;
                        }

                }

            }
            catch (Exception ex)
            {
                List<tpu> saida_erro = new List<tpu>();
                tpu err = new tpu();
                err.DataTpu = "-1";
                err.CodSubItem = ex.Message;

                saida_erro.Add(err);
                return saida_erro;
            }

            return null;
        }

        /// <summary>
        ///  Busca a lista de TPUs
        /// </summary>
        /// <param name="ano">Ano</param>
        /// <param name="fase">fase = 23</param>
        /// <param name="mes">Mês</param>
        /// <param name="onerado">Onerado: SIM,NÃO, vazio para todos</param>
        /// <param name="codSubItem">codSubItem: Opcional</param>
        /// <returns>List TPUs</returns>
        public List<tpu> get_TPUs(string ano, string fase, string mes, string onerado = "", string codSubItem = "")
        {
            try
            {
                bool sincronizar = true;
                List<tpu> lst = new List<tpu>();
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("DER.STP_SEL_DER_TPUs", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@ano", ano);
                    com.Parameters.AddWithValue("@mes", mes);
                    com.Parameters.AddWithValue("@fase", fase);
                    com.Parameters.AddWithValue("@onerado", onerado);
                    com.Parameters.AddWithValue("@codSubItem", codSubItem);

                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new tpu
                        {

                            DataTpu = rdr["DataTpu"].ToString(),
                            CodSubItem = rdr["CodSubItem"].ToString(),
                            NomeSubItem = rdr["NomeSubItem"].ToString(),
                            UnidMed = rdr["UnidMed"].ToString(),
                            PrecoUnitario = Convert.ToDecimal(rdr["PrecoUnitario"], culturePTBR),
                            Onerado = rdr["Onerado"].ToString(),
                            Fase = Convert.ToInt16(rdr["Fase"]),
                            tpu_data_atualizacao = rdr["tpu_data_atualizacao"] == DBNull.Value ? "" : rdr["tpu_data_atualizacao"].ToString()
                        });


                        // checa a data da ultima atualizacao. se nao for do dia atual, atualiza
                        if ((lst[0].tpu_data_atualizacao != "") || (sincronizar == false))
                        {
                            //if ((Convert.ToDateTime(lst[0].tpu_data_atualizacao).Day == (DateTime.Today.Day)) &&
                            //       (Convert.ToDateTime(lst[0].tpu_data_atualizacao).Month == (DateTime.Today.Month)) &&
                            //         (Convert.ToDateTime(lst[0].tpu_data_atualizacao).Year == (DateTime.Today.Year)))
                            //{
                                sincronizar = false;
                                continue;
                            //}
                            //else
                            //{
                            //    sincronizar = true;
                            //    break;
                            //}
                        }
                        else
                        {
                            sincronizar = true;
                            break;
                        }
                    }

                    if (sincronizar)
                    {
                        return DER_SincronizarTPUs(1, ano, fase, mes, onerado, codSubItem);
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