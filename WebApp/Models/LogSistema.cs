using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    /// <summary>
    /// Modelo de Registro de Log do Sistema
    /// </summary>
    public class LogSistema
    {
        /// <summary>
        /// Id do Log
        /// </summary>
        public int log_id { get; set; }

        /// <summary>
        /// Id da transação (login, seleção, exclusão, etc)
        /// </summary>
        public int tra_id { get; set; }

        /// <summary>
        /// Id do usuário responsável pela ação
        /// </summary>
        public int usu_id { get; set; }

        /// <summary>
        /// Data do evento
        /// </summary>
        public string log_data_criacao { get; set; }

        /// <summary>
        /// Id do módulo acessado
        /// </summary>
        public int mod_id { get; set; }

        /// <summary>
        /// Texto do Log
        /// </summary>
        public string log_texto { get; set; }

        /// <summary>
        /// IP do responsável pela ação
        /// </summary>
        public string log_ip { get; set; }

        /// <summary>
        /// Nome da transação
        /// </summary>
        public string tra_nome { get; set; }

        /// <summary>
        /// Nome do módulo
        /// </summary>
        public string mod_nome_modulo { get; set; }

        /// <summary>
        /// Descrição do módulo
        /// </summary>
        public string mod_descricao { get; set; }

        /// <summary>
        /// Login do usuário responsável pela ação
        /// </summary>
        public string usu_usuario { get; set; }

        /// <summary>
        /// Nome do usuário responsável pela ação
        /// </summary>
        public string usu_nome { get; set; }

        /// <summary>
        /// Texto do Log, truncado em 200 caracteres
        /// </summary>
        public string log_texto_menor { get; set; }
    }

    /// <summary>
    /// Modelo de Transacao
    /// </summary>
    public class LogTransacao
    {
        /// <summary>
        /// Id da transação (login, seleção, exclusão, etc) 
        /// </summary>
        public int tra_id { get; set; }

        /// <summary>
        /// Nome da transação (login, seleção, exclusão, etc) 
        /// </summary>
        public string tra_nome { get; set; }
    }

}