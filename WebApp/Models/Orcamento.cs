using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    // *************  ORCAMENTO  ***************************************
    /// <summary>
    /// 
    /// </summary>
    public class Orcamento
    {
        /// <summary>
        /// Id do Orçamento
        /// </summary>
        public int orc_id { get; set; }

        /// <summary>
        /// codigo do Orçamento
        /// </summary>
        public string orc_cod_orcamento { get; set; }

        /// <summary>
        /// Descrição do Orçamento
        /// </summary>
        public string orc_descricao { get; set; }

        /// <summary>
        /// Versao do Orçamento
        /// </summary>
        public string orc_versao { get; set; }


        /// <summary>
        /// Data da Criação do Orçamento
        /// </summary>
        public string orc_data_criacao { get; set; }

        /// <summary>
        ///  Data de Validade do Orçamento
        /// </summary>
        public string orc_data_validade { get; set; }

        /// <summary>
        /// id do Status do Orçamento
        /// </summary>
        public int ocs_id { get; set; }

        /// <summary>
        /// Código do Status do Orçamento
        /// </summary>
        public string ocs_codigo { get; set; }

        /// <summary>
        /// Descrição do Status do Orçamento
        /// </summary>
        public string ocs_descricao { get; set; }

        /// <summary>
        /// Valor Total do Orçamento
        /// </summary>
        public decimal orc_valor_total { get; set; }


        /// <summary>
        /// Ordens de Serviço associadas a este Orçamento
        /// </summary>
        public string orc_os_associadas { get; set; }

        /// <summary>
        /// Objetos associados às Ordens de Serviços associadas ao Orçamento
        /// </summary>
        public string orc_objetos_associados { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int orc_ativo { get; set; }
    }





    // *************  STATUS  ***************************************
    /// <summary>
    /// Modelo de Status de Orcamento
    /// </summary>
    public class OrcamentoStatus
    {
        /// <summary>
        /// Id do Status
        /// </summary>
        public int ocs_id { get; set; }

        /// <summary>
        /// Código do Status
        /// </summary>
        public string ocs_codigo { get; set; }

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string ocs_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ocs_ativo { get; set; }

    }



    // ************* FLUXO DE STATUS  ***************************************
    /// <summary>
    /// Fluxo de Status
    /// </summary>
    public class OrcamentoFluxoStatus
    {

        /// <summary>
        /// Id do Fluxo de Status 
        /// </summary>
        public int ocf_id { get; set; }

        /// <summary>
        /// Id do Status Origem 
        /// </summary>
        public int ocs_id_de { get; set; }

        /// <summary>
        /// Codigo do Status Origem  
        /// </summary>
        public string ocs_de_codigo { get; set; }

        /// <summary>
        /// Descrição do Status Origem 
        /// </summary>
        public string ocs_de_descricao { get; set; }


        /// <summary>
        /// Id do Status Destino 
        /// </summary>
        public int ocs_id_para { get; set; }

        /// <summary>
        /// Codigo do Status Destino  
        /// </summary>
        public string ocs_para_codigo { get; set; }

        /// <summary>
        /// Descrição do Status Destino 
        /// </summary>
        public string ocs_para_descricao { get; set; }

        /// <summary>
        /// Descrição do Fluxo do Status 
        /// </summary>
        public string ocf_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ocf_ativo { get; set; }

    }




}