using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{

    /// <summary>
    /// Modelo de Tipo de Reparo
    /// </summary>
    public class ReparoTipo
    {
        /// <summary>
        /// Id do Tipo
        /// </summary>
        public int rpt_id { get; set; }

        /// <summary>
        /// Código do Tipo
        /// </summary>
        public string rpt_codigo { get; set; }

        /// <summary>
        /// Descrição do Tipo
        /// </summary>
        public string rpt_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int rpt_ativo { get; set; }

    }


}