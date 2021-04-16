using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{

    // ============== SIRGeo ==============================================

    /// <summary>
    /// Token para acesso às funcionalidades do SIRGeo
    /// </summary>
    public class token
    {
        /// <summary>
        /// Número do Usuário
        /// </summary>
        public int usu_id { get; set; }

        /// <summary>
        /// Token de acesso
        /// </summary>
        public string tok_token { get; set; }

        /// <summary>
        /// Validade do token
        /// </summary>
        public DateTime tok_validade { get; set; }

        /// <summary>
        /// Mensagem de retorno do token
        /// </summary>
        public string tok_mensagem { get; set; }

        /// <summary>
        /// Valido?
        /// </summary>
        public bool tok_valido { get; set; }
    }

    // ------------ RODOVIA -----------------------
    /// <summary>
    /// Dados de Rodovia retornados
    /// </summary>
    public class Rodovia
    {
        /// <summary>
        /// Id da Rodovia
        /// </summary>
        public int rod_id { get; set; }

        /// <summary>
        /// Id da Rodovia do sistema Sigoa
        /// </summary>
        public int rod_id_sigoa { get; set; }

        /// <summary>
        /// Código da Rodovia
        /// </summary>
        public string rod_codigo { get; set; }

        /// <summary>
        /// Descrição da Rodovia
        /// </summary>
        public string rod_descricao { get; set; }

        /// <summary>
        /// Km inicial da Rodovia 
        /// </summary>
        public string rod_km_inicial { get; set; }

        /// <summary>
        /// Km final da Rodovia
        /// </summary>
        public string rod_km_final { get; set; }

        /// <summary>
        /// Extensao da Rodovia
        /// </summary>
        public string rod_km_extensao { get; set; }

        /// <summary>
        /// data da ultima atualizacao do banco
        /// </summary>
        public string rod_data_atualizacao { get; set; }

    }

    /// <summary>
    /// Lista para o Retorno da API 
    /// </summary>
    public class lstRodovias
    {
        /// <summary>
        /// Lista para o Retorno da API 
        /// </summary>
        public List<Rodovia> Rodovias { get; set; }
    }


    // ------------ OBRA DE ARTE -----------------------

    /// <summary>
    /// Dados da OAE retornados
    /// </summary>
    public class OAE
    {
        /// <summary>
        /// Id da Rodovia
        /// </summary>
        public int rod_id { get; set; }

        /// <summary>
        /// Km inicial da Rodovia 
        /// </summary>
        public string oae_km_inicial { get; set; }

        /// <summary>
        /// Km final da Rodovia
        /// </summary>
        public string oae_km_final { get; set; }

        /// <summary>
        /// Id do sentido Rodovia
        /// </summary>
        public int sen_id { get; set; }
        
        /// <summary>
        /// Id da Regional na quilometragem
        /// </summary>
        public int reg_id { get; set; }

        /// <summary>
        /// Data do Levantamento desse dado
        /// </summary>
        public string oae_data_levantamento { get; set; }

        /// <summary>
        /// Extensao da Rodovia
        /// </summary>
        public string oae_extensao { get; set; }

        /// <summary>
        /// Tipo da OAE da Rodovia
        /// </summary>
        public string oat_id { get; set; }


        /// <summary>
        /// Data da Criação desse registro da Rodovia
        /// </summary>
        public string oae_data_criacao { get; set; }


        /// <summary>
        /// Id do Objeto correspondenteno SIGOA
        /// </summary>
        public int oae_id_sigoa { get; set; }

        /// <summary>
        /// data da ultima atualizacao do banco sigoa
        /// </summary>
        public string oae_data_atualizacao { get; set; }

    }

    /// <summary>
    /// Lista para o Retorno da API 
    /// </summary>
    public class lstOAEs
    {
        /// <summary>
        /// Lista para o Retorno da API 
        /// </summary>
        public List<OAE> OAEs{ get; set; }
    }



    // ------------ DOMINIOS -----------------------

    /// <summary>
    /// Dados da REGIONAL retornados
    /// </summary>
    public class Regional
    {
        /// <summary>
        /// Id da Regional
        /// </summary>
        public int reg_id { get; set; }

        /// <summary>
        /// Código da Regional
        /// </summary>
        public string reg_codigo { get; set; }

        /// <summary>
        /// Descrição da Regional
        /// </summary>
        public string reg_descricao { get; set; }

        /// <summary>
        /// Logradouro da Regional 
        /// </summary>
        public string reg_logradouro { get; set; }

        /// <summary>
        /// Bairro do logradouro da Regional
        /// </summary>
        public string reg_bairro { get; set; }

        /// <summary>
        /// CEP da Regional
        /// </summary>
        public string reg_cep { get; set; }

        /// <summary>
        /// Email da Regional
        /// </summary>
        public string reg_email { get; set; }

        /// <summary>
        /// ddd do telefone da Regional 
        /// </summary>
        public string reg_ddd_telefone { get; set; }

        /// <summary>
        /// Telefone da Regional 
        /// </summary>
        public string reg_telefone { get; set; }

        /// <summary>
        /// ddd do telefone da CCO da Regional 
        /// </summary>
        public string reg_ddd_telefone_cco { get; set; }

        /// <summary>
        /// Telefone da CCO da Regional 
        /// </summary>
        public string reg_telefone_cco { get; set; }

        /// <summary>
        /// ddd do fax telefone da Regional 
        /// </summary>
        public string reg_ddd_fax { get; set; }

        /// <summary>
        /// fax da Regional 
        /// </summary>
        public string reg_fax { get; set; }


        /// <summary>
        /// data da ultima atualizacao do banco
        /// </summary>
        public string reg_data_atualizacao { get; set; }

    }

    /// <summary>
    /// Lista para o Retorno da API 
    /// </summary>
    public class lstRegionais
    {
        /// <summary>
        /// Lista para o Retorno da API 
        /// </summary>
        public List<Regional> Regionais { get; set; }
    }

    // ------------ Sentido de Rodovia -----------------------
    /// <summary>
    /// Dados de Sentido de Rodovia retornados
    /// </summary>
    public class SentidoRodovia
    {
        /// <summary>
        /// Id do Sentido da Rodovia
        /// </summary>
        public int sen_id { get; set; }

        /// <summary>
        /// Descrição do Sentido da Rodovia
        /// </summary>
        public string sen_descricao { get; set; }
    }

    /// <summary>
    /// Lista para o Retorno da API 
    /// </summary>
    public class lstSentidoRodovia
    {
        /// <summary>
        /// Lista para o Retorno da API 
        /// </summary>
        public List<SentidoRodovia> SentidosRodovia { get; set; }
    }




    // ------------ Tipo de OAE -----------------------
    /// <summary>
    /// Dados de Tipo de OAE retornados
    /// </summary>
    public class TipoOAE
    {
        /// <summary>
        /// Id do Tipo de OAE
        /// </summary>
        public int oat_id { get; set; }

        /// <summary>
        /// Descrição do Tipo de OAE
        /// </summary>
        public string oat_descricao { get; set; }
    }

    /// <summary>
    /// Lista para o Retorno da API 
    /// </summary>
    public class lstTipoOAE
    {
        /// <summary>
        /// Lista para o Retorno da API 
        /// </summary>
        public List<TipoOAE> TiposOAE { get; set; }
    }








    // ============== DER  ==============================================

    /// <summary>
    /// Dados de Retorno com Erro
    /// </summary>
    public class retorno_erro
    {
        /// <summary>
        /// Status: true / false 
        /// </summary>
        public bool status { get; set; }

        /// <summary>
        /// Mensagem retornada
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// Exceção do sistema
        /// </summary>
        public string exception { get; set; }
    }

    /// <summary>
    /// Dados da VDM
    /// </summary>
    public class vdm
    {
        /// <summary>
        /// Ano 
        /// </summary>
        public int vdm_ano { get; set; }

        /// <summary>
        /// Rodovia
        /// </summary>
        public string vdm_rodovia { get; set; }

        /// <summary>
        /// Número do trecho
        /// </summary>
        public int pcl_numero { get; set; }

        /// <summary>
        /// Km Inicial
        /// </summary>
        public string pcl_kminicial { get; set; }

        /// <summary>
        /// Km Final
        /// </summary>
        public string pcl_kmfinal { get; set; }

        /// <summary>
        /// Sentido 1
        /// </summary>
        public string vdm_sentido1 { get; set; }

        /// <summary>
        /// Passeio 1
        /// </summary>
        public string vdm_passeio1 { get; set; }

        /// <summary>
        /// Com 1
        /// </summary>
        public string vdm_com1 { get; set; }

        /// <summary>
        /// Moto 1
        /// </summary>
        public string vdm_moto1 { get; set; }

        /// <summary>
        /// Valor do VDM sentido 1
        /// </summary>
        public string vdm_valor1 { get; set; }

        /// <summary>
        /// Sentido 2
        /// </summary>
        public string vdm_sentido2 { get; set; }

        /// <summary>
        /// Passeio 2
        /// </summary>
        public string vdm_passeio2 { get; set; }

        /// <summary>
        /// Com 2
        /// </summary>
        public string vdm_com2 { get; set; }

        /// <summary>
        /// Moto 2
        /// </summary>
        public string vdm_moto2 { get; set; }

        /// <summary>
        /// Valor do VDM sentido 2
        /// </summary>
        public string vdm_valor2 { get; set; }

       /// <summary>
        /// Valor Passeio Bidirecional
        /// </summary>
        public string vdm_passeio_bidirecional { get; set; }

        /// <summary>
        /// Valor Com Bidirecional
        /// </summary>
        public string vdm_com_bidirecional { get; set; }

        /// <summary>
        /// Valor Moto Bidirecional
        /// </summary>
        public string vdm_moto_bidirecional { get; set; }

        /// <summary>
        /// Valor Bidirecional
        /// </summary>
        public string vdm_bidirecional { get; set; }


        /// <summary>
        /// data da ultima atualizacao do banco sigoa
        /// </summary>
        public string vdm_data_atualizacao { get; set; }
    }

    /// <summary>
    /// Parâmetros de Entrada da API VDM
    /// </summary>
    public class vdm_entrada
    {
        /// <summary>
        /// modo = "consolidado"
        /// </summary>
        public string modo { get; set; } 

        /// <summary>
        /// codigo da RODOVIA SP270 - sem o espaco
        /// </summary>
        public string rodovia { get; set; } 
        
        /// <summary>
        /// Km Inicial
        /// </summary>
        public decimal kminicial { get; set; }

        /// <summary>
        /// Km final
        /// </summary>
        public decimal kmfinal { get; set; }
    }

    /// <summary>
    /// Dados de Retorno 
    /// </summary>
    public class vdm_retorno
    {
        /// <summary>
        /// Status: true / false 
        /// </summary>
        public bool status { get; set; }

        /// <summary>
        /// Dados retornados
        /// </summary>
        public vdm_dados data { get; set; }

        /// <summary>
        /// Mensagem de erro
        /// </summary>
        public string exception { get; set; }
    }

    /// <summary>
    /// Dados Retornados
    /// </summary>
    public class vdm_dados
    {
        /// <summary>
        /// Total de registros
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Lista dos VDMs
        /// </summary>
        public List<vdm> Vdms { get; set; }
    }

    /// <summary>
    /// Dados da TPU
    /// </summary>
    public class tpu
    {
        /// <summary>
        /// Data
        /// </summary>
        public string DataTpu { get; set; }

        /// <summary>
        /// Código
        /// </summary>
        public string CodSubItem { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string NomeSubItem { get; set; }

        /// <summary>
        /// Unidade 
        /// </summary>
        public string UnidMed { get; set; }

        /// <summary>
        /// Preço Unitário
        /// </summary>
        public Decimal PrecoUnitario { get; set; }

        /// <summary>
        /// Onerado?
        /// </summary>
        public string Onerado { get; set; }


        /// <summary>
        /// Fase
        /// </summary>
        public int Fase { get; set; }

        /// data da ultima atualizacao do banco sigoa
        /// </summary>
        public string tpu_data_atualizacao { get; set; }

    }

    /// <summary>
    /// Parâmetros de entrada
    /// </summary>
    public class tpu_entrada
    {
        /// <summary>
        /// modo: tpu
        /// </summary>
        public string modo { get; set; } // = "tpu"

        /// <summary>
        /// fase: 23
        /// </summary>
        public string fase { get; set; } // = "23"

        /// <summary>
        /// Ano solicitado
        /// </summary>
        public string ano { get; set; } // = "2020"

        /// <summary>
        /// Mês solicitado
        /// </summary>
        public string mes { get; set; } // = "03"

        /// <summary>
        /// Onerado? (SIM / NÃO / VAZIO)
        /// </summary>
        public string onerado { get; set; }  // "SIM", "NÃO", "" PARA TODOS
        /// <summary>
        /// Opcional
        /// </summary>
        public string CodSubItem { get; set; }
    }

    /// <summary>
    /// Retorno da API
    /// </summary>
    public class tpu_retorno
    {
        /// <summary>
        /// status true/false
        /// </summary>
        public bool status { get; set; }

        /// <summary>
        /// Dados retornados
        /// </summary>
        public tpu_dados data { get; set; }

        /// <summary>
        /// Mensagem de erro
        /// </summary>
        public string exception { get; set; }
    }

    /// <summary>
    /// Dados retornados
    /// </summary>
    public class tpu_dados
    {
        /// <summary>
        /// Quantidade de TPUs retornadas
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Lista das TPUs
        /// </summary>
        public List<tpu> TpuPrecosSites { get; set; }
    }


}