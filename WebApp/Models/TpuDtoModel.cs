using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// TpuDtoModel
    /// </summary>
    public class TpuDtoModel
    {
        /// <summary>
        /// rtu_id
        /// </summary>
        public int rtu_id { get; set; } = 0;
        /// <summary>
        /// rpt_id
        /// </summary>
        public int rpt_id { get; set; } = 0;
        /// <summary>
        /// rpt_descricao
        /// </summary>
        public string rpt_descricao { get; set; }
        /// <summary>
        /// fon_nome
        /// </summary>
        public string fon_nome { get; set; }
        /// <summary>
        /// fon_id
        /// </summary>
        public int fon_id { get; set; } = 0;
        /// <summary>
        /// rtu_codigo_tpu
        /// </summary>
        public string rtu_codigo_tpu { get; set; }
        /// <summary>
        /// rtu_preco_unitario
        /// </summary>
        public decimal rtu_preco_unitario { get; set; }
        /// <summary>
        /// rtu_data_base
        /// </summary>
        public string rtu_data_base { get; set; } = null;
        /// <summary>
        /// rtu_fonte_txt
        /// </summary>
        public string rtu_fonte_txt { get; set; } = "";
        /// <summary>
        /// datastring
        /// </summary>
        public string datastring { get; set; }
        /// <summary>
        /// rtu_ativo
        /// </summary>
        public Boolean rtu_ativo { get; set; }

    }
}