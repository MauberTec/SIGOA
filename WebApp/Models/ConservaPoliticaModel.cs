using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// Model usado na listagem da home da Politica
    /// </summary>
    /// 
    public class ConservaPoliticaModel
    {
        /// <summary>
        /// ale_codigo
        /// </summary>
        public string ale_codigo { get; set; }
      
        /// <summary>
        /// Grupos
        /// </summary>
        public string Grupos { get; set; }

        /// <summary>
        /// tip_nome
        /// </summary>
        public string tip_nome { get; set; }
        /// <summary>
        /// Variavel
        /// </summary>
        public string Variavel { get; set; }
        /// <summary>
        /// alerta
        /// </summary>
        public int ocp_id { get; set; }
        /// <summary>
        /// alerta
        /// </summary>
        public string alerta { get; set; }
        /// <summary>
        /// servico
        /// </summary>
        public string servico { get; set; }
        /// <summary>
        /// Tipo de alerta
        /// </summary>
        public string tipo { get; set; }

    }

}