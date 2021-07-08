using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// ConservaDTO
    /// </summary>
    public class ConservaModel
    {
        /// <summary>
        /// clo_id
        /// </summary>
        public int ocp_id { get; set; }
        /// <summary>
        /// clo_nome
        /// </summary>
        public string tip_nome { get; set; }
        /// <summary>
        /// Grupo
        /// </summary>
        public string Grupo { get; set; }
        /// <summary>
        /// Variavel
        /// </summary>
        public string Variavel { get; set; }

        /// <summary>
        /// ale_codigo
        /// </summary>
        public string ale_codigo { get; set; }
        /// <summary>
        /// cgv_id
        /// </summary>
        public int cgv_id { get; set; }
        /// <summary>
        /// Alerta
        /// </summary>
        public string Alerta { get; set; }
        /// <summary>
        /// Servico
        /// </summary>
        public string Servico { get; set; }
        /// <summary>
        /// Situacao
        /// </summary>
        public int Situacao { get; set; }
    }

    /// <summary>
    /// ConservaDTO
    /// </summary>
    public class ConservaTipo
    {
        /// <summary>
        /// Id do Tipo de Conserva
        /// </summary>
        public int cot_id { get; set; }

        /// <summary>
        /// Código
        /// </summary>
        public string cot_codigo { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string cot_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int cot_ativo { get; set; }

        /// <summary>
        /// cot_criado_por
        /// </summary>
        public int cot_criado_por { get; set; }
    }

}