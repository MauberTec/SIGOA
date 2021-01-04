using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
        /// <summary>
        /// Modelo de PrecoUnitario
        /// </summary>
        public class PrecoUnitario
        {
            /// <summary>
            /// Id do PrecoUnitario
            /// </summary>
            public int tpu_id { get; set; }

            /// <summary>
            /// Id da Fase do PrecoUnitario (21-Serviços Preliminares; 22-Terraplenagem etc)
            /// </summary>
            public int fas_id { get; set; }

            /// <summary>
            /// Descrição da Fase do PrecoUnitario (Serviços Preliminares;Terraplenagem etc)
            /// </summary>
            public string fas_descricao { get; set; }

            /// <summary>
            /// Tipo de Preço  ("O" Onerado ; "D" Desonerado)
            /// </summary>
            public string tpt_id { get; set; }

            /// <summary>
            /// Descrição do Tipo de Preço  ("O" Onerado ; "D" Desonerado)
            /// </summary>
            public string tpt_descricao { get; set; }

            /// <summary>
            /// Data Base da Tabela PrecoUnitario do DER
            /// </summary>
            public string tpu_data_base_der { get; set; }

            /// <summary>
            /// Codigo DER do PrecoUnitario
            /// </summary>
            public string tpu_codigo_der { get; set; }

            /// <summary>
            /// Descrição do PrecoUnitario
            /// </summary>
            public string tpu_descricao { get; set; }

            /// <summary>
            /// Id da Unidade
            /// </summary>
            public int uni_id { get; set; }

            /// <summary>
            /// Unidade
            /// </summary>
            public string uni_unidade { get; set; }


            /// <summary>
            /// Id da Moeda
            /// </summary>
            public string moe_id { get; set; }

            /// <summary>
            /// Preço Unitario
            /// </summary>
            public string tpu_preco_unitario { get; set; }


            /// <summary>
            /// Tipo de Unidade
            /// </summary>
            public string tpu_tipo_unidade { get; set; }


            /// <summary>
            /// Preço calculado
            /// </summary>
            public string tpu_preco_calculado { get; set; }

            /// <summary>
            /// Ativo?
            /// </summary>
            public int tpu_ativo { get; set; }
        }

    /// <summary>
    /// Modelo de PrecoUnitario_Fase
    /// </summary>
    public class PrecoUnitario_Fase
        {

             /// <summary>
            /// Id da Fase do PrecoUnitario (21-Serviços Preliminares; 22-Terraplenagem etc)
            /// </summary>
            public int fas_id { get; set; }

            /// <summary>
            /// Descrição da Fase do PrecoUnitario (Serviços Preliminares;Terraplenagem etc)
            /// </summary>
            public string fas_descricao { get; set; }

        }



 }