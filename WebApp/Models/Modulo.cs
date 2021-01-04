using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// Modelo de Módulo
    /// </summary>
    public class Modulo
    {
        /// <summary>
        /// Id do Módulo
        /// </summary>
        public int mod_id { get; set; }

        /// <summary>
        /// Nome do Módulo
        /// </summary>
        public string mod_nome_modulo { get; set; }

        /// <summary>
        /// Descrição do Módulo
        /// </summary>
        public string mod_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int mod_ativo { get; set; }

        /// <summary>
        /// Id do Módulo Pai
        /// </summary>
        public int mod_pai_id { get; set; }
    }


}