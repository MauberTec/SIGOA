using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// Modelo de Item de Menu
    /// </summary>
    public class MenuModel
    {
        /// <summary>
        /// Texto do Item do Menu
        /// </summary>
        public string LinkText { get; set; }

        /// <summary>
        /// Nome do método Ação da View
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Nome do Controlador alvo
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Ordem de apresentação no Menu
        /// </summary>
        public int ordem { get; set; }

        /// <summary>
        ///  Id do Item
        /// </summary>
        public int men_menu_id { get; set; }

        /// <summary>
        /// Id Pai do Item
        /// </summary>
        public int men_pai_id { get; set; }

        /// <summary>
        /// Ícone do Item, por exemplo "fa fa-road"
        /// </summary>
        public string men_icone { get; set; }

        /// <summary>
        /// Descrição/Tooltip do Item do Menu
        /// </summary>
        public string men_descricao { get; set; }

    }
}