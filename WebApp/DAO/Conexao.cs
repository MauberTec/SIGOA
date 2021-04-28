using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApp.Helpers;

namespace WebApp.DAO
{
     /// <summary>
     ///  Camada que contém todas as string de conexão com o banco de dados
     /// </summary>
    public class Conexao
    {
        /// <summary>
        /// String descriptografada de conexão ao banco de dados
        /// </summary>
        public string strConn { get; set; }

        /// <summary>
        /// Descriptografa a string de conexão do web.config
        /// </summary>
        public Conexao()
        {
             //this.strConn = new Gerais().Decrypt(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["CONNECTION_DER"].ConnectionString);
             this.strConn = @"Data Source=DESKTOP-LVBFKSQ\SQLEXPRESS;Database=SIGOA_DESENV;Integrated Security=True;";
        }


        /// <summary>
        /// Checa se existe conexão com o banco de dados. Timeout padrão 15 segundos
        /// </summary>
        /// <returns>bool</returns>
        public bool ChecaBD()
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                try
                {

                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Retorna o nome do banco de dados da connectionstring
        /// </summary>
        /// <returns>Nome do Banco Acessado</returns>
        public string QualBD()
        {
            this.strConn = new Gerais().Decrypt(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["CONNECTION_DER"].ConnectionString);

            string [] pedacos = strConn.Split(";".ToCharArray());
            for (int i=0; i < pedacos.Length; i++)
            {
               if (pedacos[i].StartsWith("Initial Catalog"))
                 {
                    string[] valores = pedacos[i].Split("=".ToCharArray());
                    return valores[1].Replace("SIGOA", "").Replace("_", "");
                }
            }

            return "";
        }


    }
}

