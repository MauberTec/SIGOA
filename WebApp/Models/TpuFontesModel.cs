using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// TpuFontesModel
    /// </summary>
    public class TpuFontesModel
    {
        /// <summary>
        /// fon_id
        /// </summary>
        public int fon_id { get; set; }
        /// <summary>
        /// fon_prefixo
        /// </summary>
        public string fon_prefixo { get; set; }
        /// <summary>
        /// fon_nome
        /// </summary>
        public string fon_nome { get; set; }
        /// <summary>
        /// fon_ativo
        /// </summary>
        public bool fon_ativo { get; set; }
        /// <summary>
        /// fon_deletado
        /// </summary>
        public Nullable<System.DateTime> fon_deletado { get; set; }
        /// <summary>
        /// fon_data_criacao
        /// </summary>
        public System.DateTime fon_data_criacao { get; set; }
        /// <summary>
        /// fon_criado_por
        /// </summary>
        public int fon_criado_por { get; set; }
        /// <summary>
        /// fon_data_atualizacao
        /// </summary>
        public Nullable<System.DateTime> fon_data_atualizacao { get; set; }
        /// <summary>
        /// fon_atualizado_por
        /// </summary>
        public Nullable<int> fon_atualizado_por { get; set; }
    }
}