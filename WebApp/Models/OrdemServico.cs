using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
 // ************* ORDEM DE SERVICO ***************************************
   /// <summary>
    /// Modelo de OS
    /// </summary>
    public class OrdemServico
    {
        /// <summary>
        /// Id da Ordem de Servico
        /// </summary>
        public int ord_id { get; set; }

        /// <summary>
        /// Codigo da Ordem de Servico
        /// </summary>
        public string ord_codigo { get; set; }

        /// <summary>
        /// Descrição da Ordem de Servico
        /// </summary>
        public string ord_descricao { get; set; }

        /// <summary>
        /// Id da Ordem de Servico Pai
        /// </summary>
        public int ord_pai { get; set; }

        /// <summary>
        /// Id da Classe da O.S.
        /// </summary>
        public int ocl_id { get; set; }

        /// <summary>
        /// Tipo da O.S.
        /// </summary>
        public int tos_id { get; set; }

        /// <summary>
        /// Status da O.S.
        /// </summary>
        public int sos_id { get; set; }

        /// <summary>
        /// Objeto da O.S.
        /// </summary>
        public int obj_id { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ord_ativo { get; set; }

        /// <summary>
        /// Criticidade
        /// </summary>
        public decimal ord_criticidade { get; set; }

        /// <summary>
        /// Id do Contrato
        /// </summary>
        public int con_id { get; set; }

        /// <summary>
        /// Data Início Programada
        /// </summary>
        public string ord_data_inicio_programada { get; set; }

        /// <summary>
        /// Data Término Programada
        /// </summary>
        public string ord_data_termino_programada { get; set; }

        /// <summary>
        /// Data Início Executada
        /// </summary>
        public string ord_data_inicio_execucao { get; set; }

        /// <summary>
        /// Data Término Executada
        /// </summary>
        public string ord_data_termino_execucao { get; set; }

        /// <summary>
        /// Quantidade Estimada
        /// </summary>
        public Double ord_quantidade_estimada { get; set; }

        /// <summary>
        /// Unidade da Quantidade Estimada
        /// </summary>
        public int uni_id_qt_estimada { get; set; }

        /// <summary>
        /// Data Execução
        /// </summary>
        public string ord_data_execucao { get; set; }

        /// <summary>
        /// Quantidade Executada
        /// </summary>
        public Double ord_quantidade_executada { get; set; }

        /// <summary>
        /// Unidade da Quantidade Executada
        /// </summary>
        public int uni_id_qt_executada { get; set; }


        /// <summary>
        /// Custo Estimado 
        /// </summary>
        public Double ord_custo_estimado { get; set; }

        /// <summary>
        /// Custo Final 
        /// </summary>
        public Double ord_custo_final { get; set; }

        /// <summary>
        /// Aberta por
        /// </summary>
        public int ord_aberta_por { get; set; }

        /// <summary>
        /// Data Abertura
        /// </summary>
        public string ord_data_abertura { get; set; }


        /// <summary>
        /// Responsável DER
        /// </summary>
        public string ord_responsavel_der { get; set; }

        /// <summary>
        /// Responsável Fiscalização
        /// </summary>
        public string ord_responsavel_fiscalizacao { get; set; }

        /// <summary>
        /// Id do Contrato de Fiscalização
        /// </summary>
        public int con_id_fiscalizacao { get; set; }

        /// <summary>
        /// Responsável Execução
        /// </summary>
        public string ord_responsavel_execucao { get; set; }

        /// <summary>
        /// Id do Contrato de Execução
        /// </summary>
        public int con_id_execucao { get; set; }

        /// <summary>
        /// Responsável Suspenção
        /// </summary>
        public string ord_responsavel_suspensao { get; set; }

        /// <summary>
        /// Data Suspenção
        /// </summary>
        public string ord_data_suspensao { get; set; }

        /// <summary>
        /// Responsável Cancelamento
        /// </summary>
        public string ord_responsavel_cancelamento { get; set; }

        /// <summary>
        /// Data Cancelamento
        /// </summary>
        public string ord_data_cancelamento { get; set; }
        
        /// <summary>
        /// Data Reinício
        /// </summary>
        public string ord_data_reinicio { get; set; }

        /// <summary>
        /// Id do Contrato de Orçamento
        /// </summary>
        public int con_id_orcamento { get; set; }

        /// <summary>
        /// Tipo de Preço TPU Onerado/Desonerado
        /// </summary>
        public string tpt_id { get; set; }

        /// <summary>
        /// Data base DER da TPU
        /// </summary>
        public string tpu_data_base_der { get; set; }

        /// <summary>
        /// Id Do Tipo de Preço
        /// </summary>
        public string tpu_id { get; set; }

        /// <summary>
        /// Preço Unitário 
        /// </summary>
        public Double tpu_preco_unitario { get; set; }

        /// <summary>
        /// Codigo do Tipo da Ordem de Servico
        /// </summary>
        public string tos_codigo { get; set; }

        /// <summary>
        /// Descrição do Tipo da Ordem de Servico
        /// </summary>
        public string tos_descricao { get; set; }

        /// <summary>
        /// Codigo da Classe da Ordem de Servico
        /// </summary>
        public string ocl_codigo { get; set; }

        /// <summary>
        /// Descrição da Classe da Ordem de Servico
        /// </summary>
        public string ocl_descricao { get; set; }

        /// <summary>
        /// Codigo do Status da Ordem de Servico
        /// </summary>
        public string sos_codigo { get; set; }

        /// <summary>
        /// Descrição do Status da Ordem de Servico
        /// </summary>
        public string sos_descricao { get; set; }

        /// <summary>
        /// Codigo do Objeto da Ordem de Servico
        /// </summary>
        public string obj_codigo { get; set; }

        /// <summary>
        /// Descrição do Objeto da Ordem de Servico
        /// </summary>
        public string obj_descricao { get; set; }

        /// <summary>
        /// Código da Ordem de Servico Principal 
        /// </summary>
        public string ord_codigo_pai { get; set; }

        /// <summary>
        /// Descrição da Ordem de Servico Principal 
        /// </summary>
        public string ord_descricao_pai { get; set; }

        /// <summary>
        /// Codigo da TPU
        /// </summary>
        public string tpu_codigo_der { get; set; }

        /// <summary>
        /// Descrição da TPU
        /// </summary>
        public string tpu_descricao { get; set; }

        /// <summary>
        /// Codigo do Contrato de Fiscalização
        /// </summary>
        public string con_codigofiscalizacao { get; set; }

        /// <summary>
        /// Descrição do Contrato de Fiscalização
        /// </summary>
        public string con_descricaofiscalizacao { get; set; }

        /// <summary>
        /// Codigo do Contrato de Execução
        /// </summary>
        public string con_codigoexecucao { get; set; }

        /// <summary>
        /// Descrição do Contrato de Execução
        /// </summary>
        public string con_descricaoexecucao { get; set; }

        /// <summary>
        /// Codigo do Contrato de Orçamento
        /// </summary>
        public string con_codigoorcamento { get; set; }

        /// <summary>
        /// Descrição do Contrato de Orçamento
        /// </summary>
        public string con_descricaoorcamento { get; set; }

        /// <summary>
        /// Login do Usuário que abriu a O.S.
        /// </summary>
        public string ord_aberta_por_usuario { get; set; }

        /// <summary>
        /// Nome do Usuário que abriu a O.S.
        /// </summary>
        public string ord_aberta_por_nome { get; set; }



        /// <summary>
        /// Posição no grid
        /// </summary>
        public double row_numero { get; set; }

        /// <summary>
        /// Flag de Profundidade
        /// </summary>
        public double row_expandida { get; set; }

        /// <summary>
        /// Profundidade
        /// </summary>
        public double nNivel { get; set; }


        /// <summary>
        /// Número de Filhos
        /// </summary>
        public int temFilhos { get; set; }

        /// <summary>
        /// Lista dos proximos status possiveis
        /// </summary>
        public string lst_proximos_status { get; set; }



        /// <summary>
        /// Conteúdo da Aba Indicacao Servico
        /// </summary>
        public string ord_indicacao_servico { get; set; }



    }

    // ************* Classe DE ORDEM DE SERVICO ***************************************
    /// <summary>
    /// Classes de OS
    /// </summary>
    public class OSClasse
    {
        /// <summary>
        /// Id do Classe da Ordem de Servico
        /// </summary>
        public int ocl_id { get; set; }

        /// <summary>
        /// Codigo da Classe da Ordem de Servico
        /// </summary>
        public string ocl_codigo { get; set; }

        /// <summary>
        /// Descrição da Classe da Ordem de Servico
        /// </summary>
        public string ocl_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ocl_ativo { get; set; }

    }


    // ************* TIPO DE ORDEM DE SERVICO ***************************************
    /// <summary>
    /// Tipos de OS
    /// </summary>
    public class OSTipo
    {
        /// <summary>
        /// Id do Tipo da Ordem de Servico
        /// </summary>
        public int tos_id { get; set; }

        /// <summary>
        /// Codigo do Tipo da Ordem de Servico
        /// </summary>
        public string tos_codigo { get; set; }

        /// <summary>
        /// Descrição do Tipo da Ordem de Servico
        /// </summary>
        public string tos_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int tos_ativo { get; set; }

    }



// ************* STATUS DE ORDEM DE SERVICO ***************************************
    /// <summary>
    /// Status de OS
    /// </summary>
    public class OSStatus
    {
        /// <summary>
        /// Id do Status da Ordem de Servico
        /// </summary>
        public int sos_id { get; set; }

        /// <summary>
        /// Codigo do Status da Ordem de Servico
        /// </summary>
        public string sos_codigo { get; set; }

        /// <summary>
        /// Descrição do Status da Ordem de Servico
        /// </summary>
        public string sos_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int sos_ativo { get; set; }

    }


// ************* FLUXO DE STATUS DE ORDEM DE SERVICO ***************************************
    /// <summary>
    /// Fluxo de Status de OS
    /// </summary>
    public class OSFluxoStatus
    {
        
        /// <summary>
        /// Id do Fluxo de Status da Ordem de Servico
        /// </summary>
        public int fos_id { get; set; }

        /// <summary>
        /// Id do Status Origem da Ordem de Servico
        /// </summary>
        public int sos_id_de { get; set; }

        /// <summary>
        /// Codigo do Status Origem  da Ordem de Servico
        /// </summary>
        public string sos_de_codigo { get; set; }

        /// <summary>
        /// Descrição do Status Origem da Ordem de Servico
        /// </summary>
        public string sos_de_descricao { get; set; }


        /// <summary>
        /// Id do Status Destino da Ordem de Servico
        /// </summary>
        public int sos_id_para { get; set; }

        /// <summary>
        /// Codigo do Status Destino  da Ordem de Servico
        /// </summary>
        public string sos_para_codigo { get; set; }

        /// <summary>
        /// Descrição do Status Destino da Ordem de Servico
        /// </summary>
        public string sos_para_descricao { get; set; }

        /// <summary>
        /// Descrição do Fluxo do Status da Ordem de Servico
        /// </summary>
        public string fos_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int fos_ativo { get; set; }

    }




}