using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DAO
{
    /// <summary>
    /// Modelo grupo objetos grid home
    /// </summary>
    public class GrupoObjetosConserva
    {
        public int cop_id { get; set; } = 0;
        public int tip_id { get; set; }
        public int cov_id { get; set; }
        public string  OBJPAI  { get; set; }
        public string  GrupoOBJ { get; set; }
        public string  Variavel { get; set; }
        public string ale_codigo { get; set; }
        public int ale_id { get; set; }
        public string ogi_id_caracterizacao_situacao { get; set; }
        public int cot_id { get; set; }
        public string  conserva { get; set; }
    }
}