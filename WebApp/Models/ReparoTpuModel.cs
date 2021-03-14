using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ReparoTpuModel
    {
        public int rtu_id { get; set; }
        public int rpt_id { get; set; }
        public int fon_id { get; set; }
        public string rtu_fonte_txt { get; set; }
        public string rtu_tipo_tpu { get; set; }
        public string rtu_codigo_tpu { get; set; }
        public float rtu_preco_unitario { get; set; }
        public System.DateTime rtu_data_base { get; set; }
        public Nullable<bool> rtu_ativo { get; set; }
        public Nullable<System.DateTime> rtu_deletado { get; set; }
        public System.DateTime rtu_data_criacao { get; set; }
        public int rtu_criado_por { get; set; }
        public Nullable<System.DateTime> rtu_data_atualizacao { get; set; }
        public Nullable<int> rtu_atualizado_por { get; set; }
    }
}