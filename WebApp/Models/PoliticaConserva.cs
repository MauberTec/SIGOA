using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public  class tab_anomalia_alertas
    {
        public int ale_id { get; set; }
        public string ale_codigo { get; set; }
        public string ale_descricao { get; set; }
        public Nullable<bool> ale_ativo { get; set; }
        public Nullable<System.DateTime> ale_deletado { get; set; }
        public System.DateTime ale_data_criacao { get; set; }
        public int ale_criado_por { get; set; }
        public Nullable<System.DateTime> ale_data_atualizacao { get; set; }
        public Nullable<int> ale_atualizado_por { get; set; }
    }
    public class tab_conserva_grupo_objeto_variaveis
    {
        public int cgv_id { get; set; }
        public int tip_id { get; set; }
        public int cov_id { get; set; }
        public bool cgv_ativo { get; set; }
        public Nullable<System.DateTime> cgv_deletado { get; set; }
        public System.DateTime cgv_data_criacao { get; set; }
        public long cgv_criado_por { get; set; }
        public Nullable<System.DateTime> cgv_data_atualizacao { get; set; }
        public Nullable<long> cgv_atualizado_por { get; set; }
    }
    public class tab_conserva_politica
    {
        public int cop_id { get; set; }
        public Nullable<int> tip_id { get; set; }
        public int cov_id { get; set; }
        public int ogi_id_caracterizacao_situacao { get; set; }
        public int cot_id { get; set; }
        public bool cop_ativo { get; set; }
        public Nullable<System.DateTime> cop_deletado { get; set; }
        public System.DateTime cop_data_criacao { get; set; }
        public int cop_criado_por { get; set; }
        public Nullable<System.DateTime> cop_data_atualizacao { get; set; }
        public Nullable<int> cop_atualizado_por { get; set; }
    }

    public  class tab_conserva_tipos
    {

        public int cot_id { get; set; }
        public string cot_codigo { get; set; }
        public string cot_descricao { get; set; }
        public Nullable<bool> cot_ativo { get; set; }
        public Nullable<System.DateTime> cot_deletado { get; set; }
        public System.DateTime cot_data_criacao { get; set; }
        public int cot_criado_por { get; set; }
        public Nullable<System.DateTime> cot_data_atualizacao { get; set; }
        public Nullable<int> cot_atualizado_por { get; set; }
    }

    public class tab_conserva_variaveis
    {
        public int cov_id { get; set; }
        public string cov_nome { get; set; }
        public string cov_nome_tela { get; set; }
        public string cov_descricao { get; set; }
        public bool cov_ativo { get; set; }
        public Nullable<System.DateTime> cov_deletado { get; set; }
        public System.DateTime cov_data_criacao { get; set; }
        public int cov_criado_por { get; set; }
        public Nullable<System.DateTime> cov_data_atualizacao { get; set; }
        public Nullable<int> cov_atualizado_por { get; set; }
    }

}