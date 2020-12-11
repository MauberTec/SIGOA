using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// Modelo de Perfil
    /// </summary>
    public class Perfil
    {
        /// <summary>
        /// Id do Perfil
        /// </summary>
        public int per_id { get; set; }

        /// <summary>
        /// Descrição do Perfil
        /// </summary>
        public string per_descricao { get; set; }

        /// <summary>
        /// Perfil ativo?
        /// </summary>
        public int per_ativo { get; set; }
    }

    /// <summary>
    /// Módulos associados ao Perfil selecionado
    /// </summary>
    public class PerfilModulo
    {
        /// <summary>
        /// Id do Perfil selecionado
        /// </summary>
        public int per_id { get; set; }

        /// <summary>
        /// Id do Módulo associado ao Perfil selecionado
        /// </summary>
        public int mod_id { get; set; }

        /// <summary>
        /// Nome do Módulo associado ao Perfil selecionado
        /// </summary>
        public string mod_nome_modulo { get; set; }

        /// <summary>
        /// Descrição do Módulo associado ao Perfil selecionado
        /// </summary>
        public string mod_descricao { get; set; }

        /// <summary>
        /// Módulo ativo ?
        /// </summary>
        public int mod_ativo { get; set; }

        /// <summary>
        /// Leitura Ativada/Desativada
        /// </summary>
        public int mfl_leitura { get; set; }

        /// <summary>
        /// Escrita Ativada/Desativada
        /// </summary>
        public int mfl_escrita { get; set; }

        /// <summary>
        /// Exclusão Ativada/Desativada
        /// </summary>
        public int mfl_excluir { get; set; }

        /// <summary>
        /// Inserção Ativada/Desativada
        /// </summary>
        public int mfl_inserir { get; set; }

        /// <summary>
        /// Id do Módulo Pai
        /// </summary>
        public int mod_pai_id { get; set; }
    }


    /// <summary>
    /// Grupos associados ao Perfil selecionado
    /// </summary>
    public class PerfilGrupo
    {
        /// <summary>
        /// Id do Perfil selecionado
        /// </summary>
        public int per_id { get; set; }

        /// <summary>
        /// Id do Grupo associado ao Perfil selecionado
        /// </summary>
        public int gru_id { get; set; }

        /// <summary>
        /// Descrição do Grupo associado ao Perfil selecionado
        /// </summary>
        public string gru_descricao { get; set; }

        /// <summary>
        /// Grupo associado ao Perfil selecionado?
        /// </summary>
        public int gru_Associado { get; set; }
    }


    /// <summary>
    /// Usuários associados ao Perfil selecionado
    /// </summary>
    public class PerfilUsuario
    {
        /// <summary>
        /// Id do Perfil selecionado
        /// </summary>
        public int per_id { get; set; }

        /// <summary>
        /// Id do Usuário associado ao Perfil selecionado
        /// </summary>
        public int usu_id { get; set; }

        /// <summary>
        /// Login do Usuário associado ao Perfil selecionado
        /// </summary>
        public string usu_usuario { get; set; }

        /// <summary>
        /// Nome do Usuário associado ao Perfil selecionado
        /// </summary>
        public string usu_nome { get; set; }

        /// <summary>
        /// Usuário associado ao Perfil selecionado?
        /// </summary>
        public int usu_Associado { get; set; }
    }



}