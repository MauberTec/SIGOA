﻿using System;
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
                            string texto = "";
                            if (valor.Length > 0)
                            {
                                if (valor.Substring(0, 1) == "0")
                                    valor = "0";
                                else
                                    if (valor.Length <= 2)
                                        valor = "1";
                                    else
                                        texto = valor.Substring(3);                            }
                            
                            // chk_atr_id_30_48
                            listaSaida.Add(new KeyValuePair<string, string>(nomecontrole, valor));

                            //txt_atr_id_30_48
                           listaSaida.Add(new KeyValuePair<string, string>(nomecontrole.Replace("chk", "txt"), texto));


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
        /// <returns>System.Data.DataSet</returns>
        public System.Data.DataSet GruposVariaveisValores_ListAll(int? obj_id = 3, int? ord_id = -1)
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




   }
}