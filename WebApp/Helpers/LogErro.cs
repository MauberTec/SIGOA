using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Helpers
{
    /// <summary>
    /// Tipo LogErro
    /// </summary>
    public class LogErro
    {
        /// <summary>
        /// Tipo de Erro a ser registrado no sistema
        /// </summary>
        /// <param name="ex">Exceção</param>
        /// <param name="classe">Classe</param>
        /// <param name="metodo">Método</param>
        public LogErro(Exception ex, string classe, string metodo)
        {
            this.excecao = ex;
            this.processo = $"{classe} - {metodo}";
        }

        /// <summary>
        /// tipo
        /// </summary>
        public char tipo { get; } = 'A';

        /// <summary>
        /// processo
        /// </summary>
        public string processo { get; }

        /// <summary>
        /// excecao
        /// </summary>
        public Exception excecao { get; }
    }
}