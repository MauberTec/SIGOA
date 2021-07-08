using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DAO
{
    /// <summary>
    /// Modelo grupo objetos grid home
    /// </summary>
    public class Conserva_GrupoObjetos
    {
        /// <summary>
        /// Id do Tipo de Conserva
        /// </summary>
        public int cot_id { get; set; } 

        /// <summary>
        /// Codigo do Tipo de Conserva
        /// </summary>
        public string cot_codigo { get; set; }

        /// <summary>
        /// Descrição do Tipo de Conserva
        /// </summary>
        public string cot_descricao { get; set; }

        /// <summary>
        /// Id do Tipo de Objeto
        /// </summary>
        public int tip_id { get; set; }

        /// <summary>
        /// Nome do Tipo de Objeto
        /// </summary>
        public string tip_nome { get; set; }

        /// <summary>
        /// Id do Pai do Tipo de Objeto
        /// </summary>
        public int tip_pai { get; set; }

        /// <summary>
        /// Id da Politica de Conserva
        /// </summary>
        public int cop_id { get; set; } 

        /// <summary>
        /// Id da Variavel de Conserva
        /// </summary>
        public int cov_id { get; set; }

        /// <summary>
        /// Id da Variável do Grupo
        /// </summary>
        public int ogv_id { get; set; }

        /// <summary>
        /// Nome da Variável de Conserva 
        /// </summary>
        public string cov_nome { get; set; }

        /// <summary>
        /// Id do nivel de Alerta
        /// </summary>
        public int ogi_id_caracterizacao_situacao { get; set; }

        /// <summary>
        /// Descrição do nivel de Alerta (Atenção/Normal/Critico)
        /// </summary>
        public string ogi_item { get; set; }

    }








}