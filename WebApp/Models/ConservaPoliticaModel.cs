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
        /// Sub1
        /// </summary>
        public string Sub1 { get; set; }
        /// <summary>
        /// Sub2
        /// </summary>
        public string Sub2 { get; set; }
        /// <summary>
        /// Sub3
        /// </summary>
        public string Sub3 { get; set; }
        /// <summary>
        /// Grupos
        /// </summary>
        public string Grupos { get; set; }
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