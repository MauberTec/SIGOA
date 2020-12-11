using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{

    /// <summary>
    /// Modelo de Inspeção
    /// </summary>
    public class Inspecao
    {
        /// <summary>
        /// Id da Inspeção
        /// </summary>
        public int ins_id { get; set; }

        /// <summary>
        /// Id da Ordem de Servico
        /// </summary>
        public int ord_id { get; set; }

        /// <summary>
        /// Codigo da Ordem de Servico
        /// </summary>
        public string ord_codigo { get; set; }

        /// <summary>
        /// Descrição da Ordem de Servico
        /// </summary>
        public string ord_descricao { get; set; }


        /// <summary>
        /// Tipo da O.S.
        /// </summary>
        public int tos_id { get; set; }

        /// <summary>
        /// Codigo do Tipo da O.S.
        /// </summary>
        public string tos_codigo { get; set; }

        /// <summary>
        /// Descrição do Tipo da Inspeção da O.S.
        /// </summary>
        public string tos_descricao { get; set; }


        /// <summary>
        /// Status da O.S.
        /// </summary>
        public int sos_id { get; set; }

        /// <summary>
        /// Codigo do Status da Ordem de Servico
        /// </summary>
        public string sos_codigo { get; set; }

        /// <summary>
        /// Descrição do Status da Ordem de Servico
        /// </summary>
        public string sos_descricao { get; set; }

        /// <summary>
        /// Objeto da O.S. de Inspeção 
        /// </summary>
        public int obj_id { get; set; }

        /// <summary>
        /// Codigo do Objeto
        /// </summary>
        public string obj_codigo { get; set; }

        /// <summary>
        /// Descrição do Objeto
        /// </summary>
        public string obj_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ins_ativo { get; set; }


        /// <summary>
        ///  Posicao inicio da página do dataset
        /// </summary>
        public int registro_ini { get; set; }


        /// <summary>
        ///  Numero de registros do dataset
        /// </summary>
        public int total_registros { get; set; }


    }





    /// <summary>
    /// Modelo de Tipo de Inspecao
    /// </summary>
    public class InspecaoTipo
    {
        /// <summary>
        /// Id do Tipo
        /// </summary>
        public int ipt_id { get; set; }

        /// <summary>
        /// Código do Tipo
        /// </summary>
        public string ipt_codigo { get; set; }

        /// <summary>
        /// Descrição do Tipo
        /// </summary>
        public string ipt_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ipt_ativo { get; set; }

    }





}