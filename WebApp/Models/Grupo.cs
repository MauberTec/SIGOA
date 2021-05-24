using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// Modelo de Grupo de Usuário
    /// </summary>
    public class Grupo
    {
        /// <summary>
        /// Id do Grupo
        /// </summary>
        public int gru_id { get; set; }

        /// <summary>
        /// Descrição do Grupo
        /// </summary>
        public string gru_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int gru_ativo { get; set; }
    }


    /// <summary>
    /// Modelo de Perfil do Grupo selecionado 
    /// </summary>
    public class GrupoPerfil
    {
        /// <summary>
        /// Id do Grupo selecionado
        /// </summary>
        public int gru_id { get; set; }

        /// <summary>
        /// Id do Perfil associado ao Grupo selecionado
        /// </summary>
        public int per_id { get; set; }

        /// <summary>
        /// Descrição do Perfil associado ao Grupo selecionado
        /// </summary>
        public string per_descricao { get; set; }

        /// <summary>
        /// Este perfil está associado ao Grupo selecionado?
        /// </summary>
        public int per_Associado { get; set; }
    }


    /// <summary>
    /// Modelo de Usuário do Grupo selecionado 
    /// </summary>
    public class GrupoUsuario
    {
        /// <summary>
        /// Id do Grupo selecionado
        /// </summary>
        public int gru_id { get; set; }

        /// <summary>
        /// Id do Usuário associado ao Grupo selecionado
        /// </summary>
        public int usu_id { get; set; }

        /// <summary>
        /// Login do Usuário associado ao Grupo selecionado
        /// </summary>
        public string usu_usuario { get; set; }

        /// <summary>
        /// Nome do Usuário associado ao Grupo selecionado
        /// </summary>
        public string usu_nome { get; set; }

        /// <summary>
        /// Este Usuário está associado ao Grupo selecionado?
        /// </summary>
        public int usu_Associado { get; set; }
    }


    /// <summary>
    /// Modelo de Objeto do Grupo selecionado 
    /// </summary>
    public class GrupoObjeto
    {
        /// <summary>
        /// Id do Grupo selecionado
        /// </summary>
        public int gru_id { get; set; }

        /// <summary>
        /// Id do GrupoObjeto selecionado
        /// </summary>
        public int gro_id { get; set; }

        /// <summary>
        /// Id do Objeto associado ao Grupo selecionado
        /// </summary>
        public int obj_id { get; set; }

        /// <summary>
        /// Código do Objeto associado ao Grupo selecionado
        /// </summary>
        public string obj_codigo { get; set; }

        /// <summary>
        /// Descrição do Objeto associado ao Grupo selecionado
        /// </summary>
        public string obj_descricao { get; set; }
    }


}