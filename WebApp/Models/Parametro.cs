using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// Modelo de Parâmetro
    /// </summary>
    public class Parametro
    {
        /// <summary>
        /// Id do Parâmetro
        /// </summary>
        public string par_id { get; set; }

        /// <summary>
        /// Valor do Parâmetro
        /// </summary>
        public string par_valor { get; set; }

        /// <summary>
        /// Descrição do Parâmetro
        /// </summary>
        public string par_descricao { get; set; }
    }

    /// <summary>
    /// Modelo de Parâmetros de Email
    /// </summary>
    public class ParamsEmail
    {
        /// <summary>
        /// Enviar emails?
        /// </summary>
        public int Enviar_Emails;

        /// <summary>
        /// Remetente
        /// </summary>
        public string De;

        /// <summary>
        /// Destinatário(s)
        /// </summary>
        public string Para;

        /// <summary>
        /// Assunto
        /// </summary>
        public string Assunto;

        /// <summary>
        /// Texto/Corpo do Email
        /// </summary>
        public string Texto;

        /// <summary>
        /// Caminho do anexo
        /// </summary>
        public string Anexo;

        /// <summary>
        /// Lista de endereços para cópia de envio 
        /// </summary>
        public string CC;

        /// <summary>
        /// Lista de endereços para envio oculto
        /// </summary>
        public string CCO;

        /// <summary>
        /// Servidor SMTP
        /// </summary>
        public string SMTPServer;

        /// <summary>
        /// Número da porta SMTP
        /// </summary>
        public int PortaSmtp;

        /// <summary>
        /// SSL?
        /// </summary>
        public bool EnableSSL;

        /// <summary>
        /// Timeout na tentativa de envio
        /// </summary>
        public int Timeout;

        /// <summary>
        /// Email em html?
        /// </summary>
        public bool IsBodyHtml;

        /// <summary>
        /// Usuario a ser autenticado no provedor
        /// </summary>
        public string Usuario;

        /// <summary>
        /// Senha a ser autenticada no provedor
        /// </summary>
        public string Senha;

        /// <summary>
        /// Domínio
        /// </summary>
        public string Dominio;

        /// <summary>
        /// URL do Sistema 
        /// </summary>
        public string URL_SISTEMA;
    }

}