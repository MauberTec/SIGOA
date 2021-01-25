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
        /// Valor do VDM sentido 1
        /// </summary>
        public string vdm_valor1 { get; set; }

        /// <summary>
        /// Sentido 2
        /// </summary>
        public string vdm_sentido2 { get; set; }

        /// <summary>
        /// Valor do VDM sentido 2
        /// </summary>
        public string vdm_valor2 { get; set; }

        /// <summary>
        /// Valor Bidirecional
        /// </summary>
        public string vdm_bidirecional { get; set; }
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
        public Double PrecoUnitario { get; set; }

        /// <summary>
        /// Onerado?
        /// </summary>
        public string Onerado { get; set; }
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