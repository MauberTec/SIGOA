using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{

    /// <summary>
    /// Tipo de Unidade (massa, comprimento, volume, tempo, etc)
    /// </summary>
    public class Unidade_Tipo
    {
        /// <summary>
        /// Id do Tipo da unidade do item atributo
        /// </summary>
        public int unt_id { get; set; }

        /// <summary>
        /// Tipo de Unidade (massa, comprimento, volume, tempo, etc)
        /// </summary>
        public string unt_nome { get; set; }

    }


    /// <summary>
    /// Tipo de Unidade (massa, comprimento, volume, tempo, etc)
    /// </summary>
    public class Unidade
    {
        /// <summary>
        /// Id da Unidade de medida
        /// </summary>
        public int uni_id { get; set; }

        /// <summary>
        /// Id do Tipo da unidade 
        /// </summary>
        public int unt_id { get; set; }

        /// <summary>
        /// Tipo de Unidade do item (massa, comprimento, volume, tempo, etc)
        /// </summary>
        public string unt_nome { get; set; }

        /// <summary>  
        /// Lista de tipos, para preenchimento de combo
        /// </summary>
        public List<SelectListItem> lstUnidade_Tipo { get; set; }

        /// <summary>
        /// Unidade de medida
        /// </summary>
        public string uni_unidade { get; set; }

        /// <summary>
        /// Descrição da Unidade de Medida
        /// </summary>
        public string uni_descricao { get; set; }


    }


}