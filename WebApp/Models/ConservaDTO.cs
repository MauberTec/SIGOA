using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Models
{
    /// <summary>
    /// ConservaDTO
    /// </summary>
    public class ConservaDTO
    {
        /// <summary>
        /// ConservaDTO
        /// </summary>
        public ConservaDTO()
        {
            this.Conservas = new List<ConservaModel>();
        }
        /// <summary>
        /// Numero_Ogv
        /// </summary>
        public int Numero_Ogv { get; set; }

        /// <summary>
        /// Conservas
        /// </summary>
        public List<ConservaModel> Conservas { get; set; }

        
    }
}