using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{

    // ************* NOTIFICAÇÃO DE OCORRÊNCIA  ***************************************

    /// <summary>
    /// Modelo de Notificacao_Ocorrencia
    /// </summary>
    public class Notificacao_Ocorrencia
    {
        /// <summary>
        /// Id da Notificao Ocorrencia
        /// </summary>
        public int noc_id { get; set; }

        /// <summary>
        /// Id da Ordem de Serviço
        /// </summary>
        public int ord_id { get; set; }

        /// <summary>
        /// Data da Notificacao
        /// </summary>
        public string data_notificacao { get; set; }

        /// <summary>
        /// Responsavel pela Notificacao
        /// </summary>
        public string responsavel_notificacao { get; set; }

        /// <summary>
        /// Descrição da Ocorrência
        /// </summary>
        public string descricao_ocorrencia { get; set; }

        /// <summary>
        /// Solicitante
        /// </summary>
        public string solicitante { get; set; }

        /// <summary>
        /// Data da Solicitação
        /// </summary>
        public string solicitante_data { get; set; }

        /// <summary>
        /// Responsavel pelo Recebimento
        /// </summary>
        public string responsavel_recebimento { get; set; }

        /// <summary>
        /// Data do Recebimento da Ocorrencia
        /// </summary>
        public string responsavel_recebimento_data { get; set; }

        /// <summary>
        /// Identificacao OAE
        /// </summary>
        public string IdentificacaoOAE { get; set; }

        /// <summary>
        /// Nome OAE
        /// </summary>
        public string NomeOAE { get; set; }

        /// <summary>
        /// Codigo da Rodovia
        /// </summary>
        public string CodigoRodovia { get; set; }

        /// <summary>
        /// Nome da Rodovia
        /// </summary>
        public string NomeRodovia { get; set; }

        /// <summary>
        /// KM da OAE
        /// </summary>
        public string LocalizacaoKm { get; set; }

        /// <summary>
        /// Municipio da OAE
        /// </summary>
        public string Municipio { get; set; }

        /// <summary>
        /// Tipo de OAE
        /// </summary>
        public string Tipo { get; set; }




    }

}
