using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ReparoTipoModel
    {
        public int rpt_id { get; set; }
        public string rpt_codigo { get; set; }
        public string rpt_descricao { get; set; }
        public Nullable<bool> rpt_ativo { get; set; }
        public Nullable<System.DateTime> rpt_deletado { get; set; }
        public System.DateTime rpt_data_criacao { get; set; }
        public int rpt_criado_por { get; set; }
        public Nullable<System.DateTime> rpt_data_atualizacao { get; set; }
        public Nullable<int> rpt_atualizado_por { get; set; }
        public string rpt_unidade { get; set; }
        public Nullable<bool> rpt_area_acima_2m2 { get; set; }
    }
}