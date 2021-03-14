using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    // *************  ORCAMENTO  ***************************************
    /// <summary>
    /// Orçamento
    /// </summary>
    public class Orcamento
    {
        /// <summary>
        /// Id do Orçamento
        /// </summary>
        public int orc_id { get; set; }

        /// <summary>
        /// Id Pai do Orçamento
        /// </summary>
        public int orc_id_pai { get; set; }

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
        /// id do Status do Orçamento
        /// </summary>
        public int ocs_id { get; set; }

        /// <summary>
        /// Data da Criação do Orçamento
        /// </summary>
        public string orc_data_criacao { get; set; }

        /// <summary>
        ///  Data de Validade do Orçamento
        /// </summary>
        public string orc_data_validade { get; set; }

        /// <summary>
        /// Valor Total do Orçamento
        /// </summary>
        public decimal orc_valor_total { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int orc_ativo { get; set; }

        /// <summary>
        /// pri_ids_selecionados
        /// </summary>
        public string pri_ids_selecionados { get; set; }



        /// <summary>
        /// Código do Status do Orçamento
        /// </summary>
        public string ocs_codigo { get; set; }

        /// <summary>
        /// Descrição do Status do Orçamento
        /// </summary>
        public string ocs_descricao { get; set; }

        /// <summary>
        /// Ids das Prioridades associadas ao Orçamento
        /// </summary>
        public string pri_ids_associados { get; set; }

        /// <summary>
        /// Ids das OSs associadas ao Orçamento
        /// </summary>
        public string orc_ord_ids_associados { get; set; }

        /// <summary>
        /// Ordens de Serviço associadas a este Orçamento
        /// </summary>
        public string orc_os_associadas { get; set; }

        /// <summary>
        /// Ids dos Objetos associados às Ordens de Serviços associadas ao Orçamento
        /// </summary>
        public string orc_obj_ids_associados { get; set; }

        /// <summary>
        /// Objetos associados às Ordens de Serviços associadas ao Orçamento
        /// </summary>
        public string orc_objetos_associados { get; set; }


        /// <summary>
        /// lista de Status de Orçamento
        /// </summary>
        public string lstStatusOrcamento { get; set; }


    }


    /// <summary>
    /// Detalhes do Orçamento
    /// </summary>
    public class OrcamentoDetalhes
    {

        /// <summary>
        /// Id do Reparo Orçamento
        /// </summary>
        public int ore_id { get; set; }

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
        ///  Data de Validade do Orçamento
        /// </summary>
        public string orc_data_validade { get; set; }

        /// <summary>
        /// Valor Total do Orçamento
        /// </summary>
        public decimal orc_valor_total { get; set; }

        /// <summary>
        /// Id Pai do Orçamento
        /// </summary>
        public int orc_id_pai { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int orc_ativo { get; set; }


        /// <summary>
        /// id do Status do Orçamento
        /// </summary>
        public int ocs_id { get; set; }

        /// <summary>
        /// codigo do Status do Orçamento
        /// </summary>
        public string ocs_codigo { get; set; } 
        
        /// <summary>
        /// Descrição do Status do Orçamento
        /// </summary>
        public string ocs_descricao { get; set; }

        /// <summary>
        /// id do Objeto OAE do Orçamento
        /// </summary>
        public int obj_id_oae { get; set; }

        /// <summary>
        /// codigo do Objeto OAE do Orçamento
        /// </summary>
        public string obj_codigoOAE { get; set; } 
        
        /// <summary>
        /// Descrição do Objeto OAE do Orçamento
        /// </summary>
        public string obj_descricaoOAE { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ore_ativo { get; set; }

        /// <summary>
        /// id do Objeto do Orçamento
        /// </summary>
        public int obj_idElemento { get; set; }

        /// <summary>
        /// codigo do Objeto do Orçamento
        /// </summary>
        public string obj_codigoElemento { get; set; } 
        
        /// <summary>
        /// Descrição do Objeto do Orçamento
        /// </summary>
        public string obj_descricaoElemento { get; set; }

        /// <summary>
        /// Id da Anomalia 
        /// </summary>
        public int ian_id { get; set; }

        /// <summary>
        /// Número da Anomalia 
        /// </summary>
        public string ian_numero { get; set; }

        /// <summary>
        /// Ordem sequencial das anomalias 
        /// </summary>
        public int ian_ordem_apresentacao { get; set; }

        /// <summary>
        /// Id do tipo de anomalia 
        /// </summary>
        public int atp_id { get; set; }

        /// <summary>
        /// Código do tipo de anomalia
        /// </summary>
        public string atp_codigo { get; set; }

        /// <summary>
        /// Descrição do tipo de anomalia
        /// </summary>
        public string atp_descricao { get; set; }

        /// <summary>
        /// Sigla da anomalia  
        /// </summary>
        public string ian_sigla { get; set; }

        /// <summary>
        /// Quantidade da anomalia  
        /// </summary>
        public decimal ian_quantidade { get; set; }

        /// <summary>
        /// Id da legenda
        /// </summary>
        public int leg_id { get; set; }

        /// <summary>
        /// Código da legenda da anomalia
        /// </summary>
        public string leg_codigo { get; set; }

        /// <summary>
        /// Descrição da legenda da anomalia
        /// </summary>
        public string leg_descricao { get; set; }

        /// <summary>
        /// Id do nivel de alerta 
        /// </summary>
        public int ale_id { get; set; }

        /// <summary>
        /// Código do alerta da anomalia
        /// </summary>
        public string ale_codigo { get; set; }

        /// <summary>
        /// Descrição do Alerta de anomalia
        /// </summary>
        public string ale_descricao { get; set; }

        /// <summary>
        /// Id de causa provável
        /// </summary>
        public int aca_id { get; set; }

        /// <summary>
        /// Código da causa da anomalia
        /// </summary>
        public string aca_codigo { get; set; }

        /// <summary>
        /// Descrição da causa da anomalia
        /// </summary>
        public string aca_descricao { get; set; }


        /// <summary>
        /// Id do reparo sugerido
        /// </summary>
        public int rpt_id_sugerido { get; set; }

        /// <summary>
        /// Codigo do reparo sugerido
        /// </summary>
        public string rpt_id_sugerido_codigo { get; set; }

        /// <summary>
        /// Descricao do reparo sugerido
        /// </summary>
        public string rpt_id_sugerido_descricao { get; set; }

        /// <summary>
        /// Unidade da Quantidade do reparo sugerido
        /// </summary>
        public string rpt_id_sugerido_unidade { get; set; }

        /// <summary>
        /// Quantidade do reparo sugerido
        /// </summary>
        public decimal ian_quantidade_sugerida { get; set; }

        /// <summary>
        /// Id do reparo adotado
        /// </summary>
        public int rpt_id_adotado { get; set; }

        /// <summary>
        /// Codigo do reparo adotado
        /// </summary>
        public string rpt_id_adotado_codigo { get; set; }

        /// <summary>
        /// Descricao do reparo adotado
        /// </summary>
        public string rpt_id_adotado_descricao{ get; set; }

        /// <summary>
        /// Unidade da Quantidade do reparo adotada
        /// </summary>
        public string rpt_id_adotado_unidade { get; set; }

        /// <summary>
        /// Quantidade do reparo adotado
        /// </summary>
        public decimal ian_quantidade_adotada { get; set; }


        /// <summary>
        /// Preço unitário da TPU para valor sugerido
        /// </summary>
        public decimal rtu_preco_unitario_sugerido { get; set; }

        /// <summary>
        /// Valor total da TPU do reparo sugerido
        /// </summary>
        public decimal rtu_valor_total_sugerido { get; set; }
            
        /// <summary>
        /// Preço unitário da TPU para valor adotado
        /// </summary>
        public decimal rtu_preco_unitario_adotado { get; set; }

        /// <summary>
        /// Valor total da TPU do reparo adotado
        /// </summary>
        public decimal rtu_valor_total_adotado { get; set; }

        /// <summary>
        /// Ids dos Objetos associados às Ordens de Serviços associadas ao Orçamento
        /// </summary>
        public string orc_obj_ids_associados { get; set; }

        /// <summary>
        /// Objetos associados às Ordens de Serviços associadas ao Orçamento
        /// </summary>
        public string orc_objetos_associados { get; set; }

        /// <summary>
        /// Ids das Prioridades associadas ao Orçamento
        /// </summary>
        public string pri_ids_associados { get; set; }


        /// <summary>
        /// lista de Status de Orçamento
        /// </summary>
        public string lstStatusOrcamento { get; set; }

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