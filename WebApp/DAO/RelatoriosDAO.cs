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
    /// Datasources dos Relatorios e Fichas
    /// </summary>
    public class RelatoriosDAO : Conexao
    {
        // *************** Relatorios  *************************************************************

        /// <summary>
        ///     Lista de todos os labels e textboxes do relatorio
        /// </summary>
        /// <param name="obj_id">Id do objeto</param>
        /// <param name="ord_id">Id da O.S pertinente ao objeto</param>
        /// <returns>Dataset</returns>
        public System.Data.DataSet FICHA_INSPECAO_CADASTRAL2(int? obj_id = null, int? ord_id = null)
        {
            try
            {
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_FICHA_INSPECAO_CADASTRAL", con);
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
        ///     Lista de todos os labels e textboxes do relatorio
        /// </summary>
        /// <param name="obj_id">Id do objeto</param>
        /// <param name="ord_id">Id da O.S pertinente ao objeto</param>
        /// <returns>Dataset</returns>
        public System.Data.DataSet FICHA_INSPECAO_CADASTRAL(int? obj_id = null, int? ord_id = null)
        {
            try
            {
                DataTable dt1 = new System.Data.DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                // carrega os dados 
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_INSPECAO_ATRIBUTOS_VALORES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@ord_id", ord_id);

                    adapter.SelectCommand = com;
                    adapter.Fill(dt1);
                }

                 // trata os dados e coloca em lista key value (depois faz a transposicao já no datatable)
                var listaSaida = new List<KeyValuePair<string, string>>();

                // cria as colunas
                string atr_apresentacao_itens = "";
                string nomeColuna = "";
                string valor = "";
                for (int li = 0; li < dt1.Rows.Count; li++)
                {
                    atr_apresentacao_itens = dt1.Rows[li]["atr_apresentacao_itens"].ToString().Trim();
                    nomeColuna = dt1.Rows[li]["atv_controle"].ToString().Trim();
                    valor = dt1.Rows[li]["atv_valor"].ToString().Trim();

                    switch (atr_apresentacao_itens)
                    {
                        case "checkbox":
                            if ((valor == "") || (valor == "-1"))
                                valor = "0:";

                           string nomecontrole = (nomeColuna + "_" + dt1.Rows[li]["ati_ids"].ToString().Trim());

                            // localiza e adiciona o texto do rotulo 
                            if (dt1.Rows[li]["atr_itens_todos"].ToString().Trim().Length > 0)
                            {
                                string[] itens1 = dt1.Rows[li]["atr_itens_todos"].ToString().Trim().Split(';');

                                // procura no array o texto respectivo
                                int selId = Convert.ToInt32(dt1.Rows[li]["ati_ids"]);
                                valor = "";
                                for (int v = 0; v < itens1.Length; v++)
                                {
                                    if (selId == Convert.ToInt32(itens1[v].Trim().Substring(0, 3)))
                                    {
                                        valor = itens1[v].Trim().Substring(3);
                                        break;
                                    }
                                }

                                // lblatr_id_30_48
                                listaSaida.Add(new KeyValuePair<string, string>(nomecontrole.Replace("chk_", "lbl"), valor));
                            }

                            valor = dt1.Rows[li]["atv_valor"].ToString().Trim();
                            if (valor.Length > 0)
                            {
                                if ((valor.Substring(0, 1) == "0"))
                                    valor = "0";
                                else
                                    if (valor.Length <= 2)
                                        valor = "1";
                                    else
                                        valor = valor.Substring(2);
                            }
                            else 
                             if (valor == "")
                                valor = "0";

                            // chk_atr_id_30_48
                            listaSaida.Add(new KeyValuePair<string, string>(nomecontrole, valor));

                            //txt_atr_id_30_48
                          // listaSaida.Add(new KeyValuePair<string, string>(nomecontrole.Replace("chk", "txt"), texto));


                            break;

                        case "combobox":
                            // adiciona o lbl
                            listaSaida.Add(new KeyValuePair<string, string>(nomeColuna.Replace("cmb_", "lbl"), dt1.Rows[li]["atr_atributo_nome"].ToString().Trim()));

                            // localiza e adiciona o valor txt
                            string[] itens = dt1.Rows[li]["atr_itens_todos"].ToString().Trim().Split(';');
                            // procura no array o valor respectivo
                            valor =  "";
                            for (int v = 0; v < itens.Length; v++)
                                if (dt1.Rows[li]["atv_valor"].ToString().Trim() == itens[v].Trim().Substring(0, 3))
                                {
                                    valor = itens[v].Trim().Substring(3);
                                    break;
                                }


                            listaSaida.Add(new KeyValuePair<string, string>(nomeColuna.Replace("cmb", "txt"), valor));
                            break;

                        case "":
                            string rotulo =  dt1.Rows[li]["atr_atributo_nome"].ToString().Trim();
                            if (rotulo.IndexOf('|') > 0)
                                rotulo = rotulo.Substring(rotulo.IndexOf('|') + 1);

                            if (nomeColuna.StartsWith("txt_"))
                                listaSaida.Add(new KeyValuePair<string, string>(nomeColuna, valor));
                            else
                                if (nomeColuna.StartsWith("lbl_"))
                                    listaSaida.Add(new KeyValuePair<string, string>(nomeColuna, valor));
                            else
                               if (nomeColuna.StartsWith("lbl"))
                            {
                                listaSaida.Add(new KeyValuePair<string, string>(nomeColuna, rotulo));
                                listaSaida.Add(new KeyValuePair<string, string>(nomeColuna.Replace("lbl", "txt_"), valor));
                            }
                            break;
                    }
                }

                // transpõe a lista para um datatable
                DataTable dt2 = new DataTable();
                // cria as colunas
                for (var i = 0; i < listaSaida.Count; i++)
                  if (!dt2.Columns.Contains(listaSaida[i].Key))
                    dt2.Columns.Add(listaSaida[i].Key);
                dt2.AcceptChanges();

                DataRow row = dt2.NewRow();
                for (var i = 0; i < listaSaida.Count; i++)
                    if (dt2.Columns.Contains(listaSaida[i].Key).ToString().Trim() != "")
                       row[listaSaida[i].Key] = listaSaida[i].Value;

                dt2.Rows.Add(row);
                dt2.AcceptChanges();

                DataSet dss = new DataSet();
                dss.Tables.Add(dt2);
                    
                return dss;

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
        /// <param name="ehProvidencia">Flag para tela Providências</param>
        /// <param name="filtro_prt_id">Filtro id da Providência</param>
        /// <returns>System.Data.DataSet</returns>
        public System.Data.DataSet GruposVariaveisValores_ListAll(int? obj_id = 3, int? ord_id = -1, int? ehProvidencia = 0, int? filtro_prt_id = 0)
        {
            try
            {
                string qualProcedure = "STP_SEL_OBJETO_GRUPO_OBJETO_VARIAVEIS_VALORES";
                if (ehProvidencia == 1)
                    qualProcedure = "STP_SEL_INSPECAO_GRUPO_OBJETO_VARIAVEIS_VALORES_PROVIDENCIAS";

                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand(qualProcedure, con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    if (ehProvidencia == 0)
                        com.Parameters.AddWithValue("@obj_id", obj_id);

                    com.Parameters.AddWithValue("@ord_id", ord_id);

                    if (ehProvidencia == 1)
                       com.Parameters.AddWithValue("@filtro_prt_id", filtro_prt_id);

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
        ///     Lista de todos textboxes do relatorio
        /// </summary>
        /// <param name="ord_id">Id da O.S pertinente ao objeto</param>
        /// <returns>DataTable</returns>
        public System.Data.DataTable FICHA_NotificacaoOcorrencia(int ord_id)
        {
            try
            {
                DataTable dt1 = new System.Data.DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                // carrega os dados 
                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_NOTIFICACAO_OCORRENCIAS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@ord_id", ord_id);

                    adapter.SelectCommand = com;
                    adapter.Fill(dt1);
                }

 
                return dt1;

            }
            catch (Exception ex)
            {
                int id = 0;
                new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Lista Grupos/Variáveis do Objeto Selecionado
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param> 
        /// <param name="apt_id">Id da Providência selecionada, -1 para todos</param> 
        /// <returns>System.Data.DataSet</returns>
        public System.Data.DataSet FICHA_ESPECIAL_PROVIDENCIAS(int ord_id, int apt_id)
        {
            try
            {
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_INSPECAO_ANOMALIAS_VALORES_PROVIDENCIAS", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@ord_id", ord_id);
                    com.Parameters.AddWithValue("@apt_id", apt_id);

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


        // **************** RELATORIOS DO SISTEMA ***************************************************************************************

        /// <summary>
        /// Lista de Objetos Priorizados
        /// </summary>
        /// <param name="CodRodovia">Filtro por Codigo da Rodovia</param>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovia</param>
        /// <param name="FiltroidRegionais">Filtro por id de  Regional</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objeto</param>
        /// <param name="Filtro_data_De">Filtro por Data Inicial</param>
        /// <param name="Filtro_data_Ate">Filtro por Data final</param>
        /// <param name="somenteINSP_ESPECIAIS">Filtro por Inspecao Especial</param>
        /// <param name="LstRegionais">Lista de Regionais obtidas no SirGeo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <returns>DataSet</returns>
        public System.Data.DataSet ObjPriorizacao_Ds(string CodRodovia,
                                                      string FiltroidRodovias = "", string FiltroidRegionais = "", string FiltroidObjetos = "", string Filtro_data_De = "", string Filtro_data_Ate = "",
                                                        int? somenteINSP_ESPECIAIS = 0,
                                                        string LstRegionais = "",
                                                        int? usu_id = null)
        {
            try
            {
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_OBJETOS_PRIORIZACAO", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@CodRodovia", CodRodovia);
                    com.Parameters.AddWithValue("@FiltroidRodovias", FiltroidRodovias);
                    com.Parameters.AddWithValue("@FiltroidRegionais", FiltroidRegionais);
                    com.Parameters.AddWithValue("@FiltroidObjetos", FiltroidObjetos);
                    com.Parameters.AddWithValue("@Filtro_data_De", Filtro_data_De);
                    com.Parameters.AddWithValue("@Filtro_data_Ate", Filtro_data_Ate);
                    com.Parameters.AddWithValue("@somenteINSP_ESPECIAIS", somenteINSP_ESPECIAIS);
                    com.Parameters.AddWithValue("@lstRegionais", LstRegionais);
                    com.Parameters.AddWithValue("@usu_id", usu_id);

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
        /// Lista de Objetos Priorizados
        /// </summary>
        /// <param name="CodRodovia">Filtro por Codigo da Rodovia</param>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovia</param>
        /// <param name="FiltroidRegionais">Filtro por id de  Regional</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objeto</param>
        /// <param name="Filtro_data_De">Filtro por Data Inicial</param>
        /// <param name="Filtro_data_Ate">Filtro por Data final</param>
        /// <param name="somenteINSP_ESPECIAIS">Filtro por Inspecao Especial</param>
        /// <param name="LstRegionais">Lista de Regionais obtidas no SirGeo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <returns>DataSet</returns>
        public System.Data.DataSet Objetos_Relatorio_Acoes_Ds(string CodRodovia,
                                                      string FiltroidRodovias = "", string FiltroidRegionais = "", string FiltroidObjetos = "", string Filtro_data_De = "", string Filtro_data_Ate = "",
                                                        int? somenteINSP_ESPECIAIS = 0,
                                                        string LstRegionais = "",
                                                        int? usu_id = null)
        {
            try
            {
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_RELATORIO_ACOES", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@CodRodovia", CodRodovia);
                    com.Parameters.AddWithValue("@FiltroidRodovias", FiltroidRodovias);
                    com.Parameters.AddWithValue("@FiltroidRegionais", FiltroidRegionais);
                    com.Parameters.AddWithValue("@FiltroidObjetos", FiltroidObjetos);
                    com.Parameters.AddWithValue("@Filtro_data_De", Filtro_data_De);
                    com.Parameters.AddWithValue("@Filtro_data_Ate", Filtro_data_Ate);
                    com.Parameters.AddWithValue("@somenteINSP_ESPECIAIS", somenteINSP_ESPECIAIS);
                    com.Parameters.AddWithValue("@lstRegionais", LstRegionais);
                    com.Parameters.AddWithValue("@usu_id", usu_id);

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
        /// Lista de Objetos Priorizados
        /// </summary>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovia</param>
        /// <param name="FiltroidRegionais">Filtro por id de  Regional</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objeto</param>
        /// <param name="Filtro_data_De">Filtro por Data Inicial</param>
        /// <param name="Filtro_data_Ate">Filtro por Data final</param>
        /// <param name="LstRegionais">Lista de Regionais obtidas no SirGeo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <returns>DataSet</returns>
        public System.Data.DataSet PerformanceOAEs_Ds(string FiltroidRodovias = "", string FiltroidRegionais = "", string FiltroidObjetos = "", string Filtro_data_De = "", string Filtro_data_Ate = "",
                                                      string LstRegionais = "",
                                                      int? usu_id = null)
        {
            try
            {
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_RELATORIO_PERFORMANCE_OAEs", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@FiltroidRodovias", FiltroidRodovias);
                    com.Parameters.AddWithValue("@FiltroidRegionais", FiltroidRegionais);
                    com.Parameters.AddWithValue("@FiltroidObjetos", FiltroidObjetos);
                    com.Parameters.AddWithValue("@Filtro_data_De", Filtro_data_De);
                    com.Parameters.AddWithValue("@Filtro_data_Ate", Filtro_data_Ate);
                    com.Parameters.AddWithValue("@lstRegionais", LstRegionais);
                    com.Parameters.AddWithValue("@usu_id", usu_id);

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
        /// Lista de O.S.s
        /// </summary>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovia</param>
        /// <param name="FiltroidRegionais">Filtro por id de  Regional</param>
        /// <param name="FiltroTiposOS">Filtro por Tipo de O.S.</param>
        /// <param name="FiltroStatusOS">Filtro por Status de O.S.</param>
        /// <param name="Filtro_data">Filtro por tipo de Data</param>
        /// <param name="Filtro_data_De">Filtro por Data Inicial</param>
        /// <param name="Filtro_data_Ate">Filtro por Data final</param>
        /// <param name="LstRegionais">Lista de Regionais obtidas no SirGeo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <returns>DataSet</returns>
        public System.Data.DataSet OSs_Ds(string FiltroidRodovias = "", string FiltroidRegionais = "",
                                                        string FiltroTiposOS = "", string FiltroStatusOS = "",
                                                        string Filtro_data = "", string Filtro_data_De = "", string Filtro_data_Ate = "",
                                                        string LstRegionais = "",
                                                        int? usu_id = null)
        {
            try
            {
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_RELATORIO_OSs", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@FiltroidRodovias", FiltroidRodovias);
                    com.Parameters.AddWithValue("@FiltroidRegionais", FiltroidRegionais);
                    com.Parameters.AddWithValue("@FiltroTiposOS", FiltroTiposOS);
                    com.Parameters.AddWithValue("@FiltroStatusOS", FiltroStatusOS);
                    com.Parameters.AddWithValue("@FiltroData", Filtro_data);
                    com.Parameters.AddWithValue("@Filtro_data_De", Filtro_data_De);
                    com.Parameters.AddWithValue("@Filtro_data_Ate", Filtro_data_Ate);
                    com.Parameters.AddWithValue("@lstRegionais", LstRegionais);
                    com.Parameters.AddWithValue("@usu_id", usu_id);

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
        /// Lista de Anomalias aguardando Reparos
        /// </summary>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovia</param>
        /// <param name="FiltroidRegionais">Filtro por id de  Regional</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objeto</param>
        /// <param name="LstRegionais">Lista de Regionais obtidas no SirGeo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <returns>DataSet</returns>
        public System.Data.DataSet AnomaliasAgRep_Ds(string FiltroidRodovias = "", string FiltroidRegionais = "", 
                                                      string FiltroidObjetos = "", string LstRegionais = "",
                                                      int? usu_id = null)
        {
            try
            {
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_RELATORIO_ANOMALIAS_AG_REP", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@FiltroidRodovias", FiltroidRodovias);
                    com.Parameters.AddWithValue("@FiltroidRegionais", FiltroidRegionais);
                    com.Parameters.AddWithValue("@FiltroidObjetos", FiltroidObjetos);
                    com.Parameters.AddWithValue("@lstRegionais", LstRegionais);
                    com.Parameters.AddWithValue("@usu_id", usu_id);

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
        /// Lista de OS RECUP
        /// </summary>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovia</param>
        /// <param name="FiltroidRegionais">Filtro por id de  Regional</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objeto</param>
        /// <param name="LstRegionais">Lista de Regionais obtidas no SirGeo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <returns>DataSet</returns>
        public System.Data.DataSet OSRecup_Ds(string FiltroidRodovias = "", string FiltroidRegionais = "",
                                                      string FiltroidObjetos = "", string LstRegionais = "",
                                                      int? usu_id = null)
        {
            try
            {
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_RELATORIO_OS_RECUP", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@FiltroidRodovias", FiltroidRodovias);
                    com.Parameters.AddWithValue("@FiltroidRegionais", FiltroidRegionais);
                    com.Parameters.AddWithValue("@FiltroidObjetos", FiltroidObjetos);
                    com.Parameters.AddWithValue("@lstRegionais", LstRegionais);
                    com.Parameters.AddWithValue("@usu_id", usu_id);

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
        /// Lista de orcamentos aprovados
        /// </summary>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovia</param>
        /// <param name="FiltroidRegionais">Filtro por id de  Regional</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objeto</param>
        /// <param name="LstRegionais">Lista de Regionais obtidas no SirGeo</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <returns>DataSet</returns>
        public System.Data.DataSet OrcamentosApr_Ds(string FiltroidRodovias = "", string FiltroidRegionais = "",
                                                      string FiltroidObjetos = "", string LstRegionais = "",
                                                      int? usu_id = null)
        {
            try
            {
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(strConn))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("STP_SEL_RELATORIO_ORCAMENTOS_APR", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@FiltroidRodovias", FiltroidRodovias);
                    com.Parameters.AddWithValue("@FiltroidRegionais", FiltroidRegionais);
                    com.Parameters.AddWithValue("@FiltroidObjetos", FiltroidObjetos);
                    com.Parameters.AddWithValue("@lstRegionais", LstRegionais);
                    com.Parameters.AddWithValue("@usu_id", usu_id);

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



    }
}