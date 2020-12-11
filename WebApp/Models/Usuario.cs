using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// Modelo de Usuário
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Id do Usuário
        /// </summary>
        public int usu_id { get; set; }

        /// <summary>
        /// Nome do Usuário
        /// </summary>
        public string usu_nome { get; set; }

        /// <summary>
        /// Login do Usuário
        /// </summary>
        public string usu_usuario { get; set; }

        /// <summary>
        /// Email do Usuário
        /// </summary>
        public string usu_email { get; set; }

        /// <summary>
        /// Senha do Usuário
        /// </summary>
        public string usu_senha { get; set; }

        /// <summary>
        /// IP do Usuário
        /// </summary>
        public string usu_ip { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int usu_ativo { get; set; }

        /// <summary>
        /// Flag indicativo para o usuario trocar a senha
        /// </summary>
        public int usu_trocar_senha { get; set; }


        /// <summary>
        /// Nome do arquivo de imagem do Usuário
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public string usu_foto { get; set; }

        /// <summary>
        /// Lista dos Menus permitidos ao Usuário
        /// </summary>
        public List<MenuModel> lstMenus { get; set; }

        /// <summary>
        /// Lista de Permissoes do Usuário  
        /// </summary>
        public List<UsuarioPermissoes> lstUsuarioPermissoes { get; set; }
    }


    /// <summary>
    /// Modelo de Permissao de Usuário
    /// </summary>
    public class UsuarioPermissoes
    {
        /// <summary>
        /// Id do Módulo
        /// </summary>
        public int mod_id { get; set; }

        /// <summary>
        /// Id do Perfil
        /// </summary>
        public int per_id { get; set; }

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
    }


    /// <summary>
    /// Modelo de Perfil associado ao Usuário
    /// </summary>
    public class UsuarioPerfil
    {
        /// <summary>
        /// Id do Usuário selecionado
        /// </summary>
        public int usu_id { get; set; }

        /// <summary>
        /// Id do Perfil associado ao Usuário selecionado
        /// </summary>
        public int per_id { get; set; }

        /// <summary>
        /// Descrição do Perfil associado ao Usuário selecionado
        /// </summary>
        public string per_descricao { get; set; }

        /// <summary>
        /// Perfil está associado?
        /// </summary>
        public int per_Associado { get; set; }
    }


    /// <summary>
    /// Modelo de Grupo associado ao Usuário
    /// </summary>
    public class UsuarioGrupo
    {
        /// <summary>
        /// Id do Usuário selecionado
        /// </summary>
        public int usu_id { get; set; }

        /// <summary>
        /// Id do Grupo associado ao Usuário selecionado
        /// </summary>
        public int gru_id { get; set; }

        /// <summary>
        /// Descrição do Grupo associado ao Usuário selecionado
        /// </summary>
        public string gru_descricao { get; set; }

        /// <summary>
        /// Grupo associado ao Usuário selecionado?
        /// </summary>
        public int gru_Associado { get; set; }
    }

    /// <summary>
    /// Modelo de Imagem do Usuário Selecionado
    /// </summary>
    public class UsuarioFoto
    {
        /// <summary>
        /// Id do Usuário selecionado
        /// </summary>
        public int usu_id { get; set; }

        /// <summary>
        /// Nome da Foto Anterior
        /// </summary>
        public string FotoAnterior { get; set; }

        /// <summary>
        /// Arquivo da nova Imagem
        /// </summary>
        public HttpPostedFileWrapper ImageFile { get; set; }
    }



}
