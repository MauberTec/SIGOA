using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    /// <summary>
    /// Modelo de Documento
    /// </summary>
    public class Documento
    {
        /// <summary>
        /// Id do Documento
        /// </summary>
        public int doc_id { get; set; }

        /// <summary>
        /// Código do Documento
        /// </summary>
        public string doc_codigo { get; set; }

        /// <summary>
        /// Descrição do Documento
        /// </summary>
        public string doc_descricao { get; set; }

        /// <summary>
        /// Caminho do Documento
        /// </summary>
        public string doc_caminho { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int doc_ativo { get; set; }


        /// <summary>
        /// ID da Classe de Documento
        /// </summary>
        public int dcl_id { get; set; }

        /// <summary>
        /// Codigo da Classe
        /// </summary>
        public string dcl_codigo { get; set; }

        /// <summary>
        /// Descrição da Classe de Documento
        /// </summary>
        public string dcl_descricao { get; set; }

        /// <summary>
        /// É documento de referência?
        /// </summary>
        public int dos_referencia { get; set; }

        /// <summary>
        /// Id do Tipo de Documento
        /// </summary>
        public string tpd_id { get; set; }

        /// <summary>
        /// Descrição do Tipo de Documento
        /// </summary>
        public string tpd_descricao { get; set; }

        /// <summary>
        /// Classe de Projeto do Documento
        /// </summary>
        public string doc_classe_projeto { get; set; }

        /// <summary>
        /// Sequencial do Documento
        /// </summary>
        public string doc_sequencial { get; set; }

        /// <summary>
        /// Subnível 2 (Documento Tecnico Especifico) parte 1 
        /// </summary>
        public string doc_subNivel21 { get; set; }  // para Documento Tecnico Especifico

        /// <summary>
        /// Subnível 2 (Documento Tecnico Especifico) parte 2, primeiro trecho(Km Inicial por exemplo)
        /// </summary>
        public string doc_subNivel22a { get; set; } // para Documento Tecnico Especifico

        /// <summary>
        /// Subnível 2 (Documento Tecnico Especifico) parte 2, segundo trecho(Km Final por exemplo)
        /// </summary>
        public string doc_subNivel22b { get; set; } // para Documento Tecnico Especifico

        /// <summary>
        /// Subnível 2 (Documento Tecnico Especifico) parte 3
        /// </summary>
        public string doc_subNivel23 { get; set; }  // para Documento Tecnico Especifico

        /// <summary>
        /// Revisao do Documento
        /// </summary>
        public string doc_revisao { get; set; }


        /// <summary>
        /// Lista de Classe de Projeto, para preenchimento de combo
        /// </summary>
        public List<SelectListItem> lstClasseProjeto { get; set; }


        /// <summary>
        /// Lista de Classe de Documento, para preenchimento de combo
        /// </summary>
        public List<SelectListItem> lstDocClasse { get; set; }


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
    /// Modelo de Tipo de Documento
    /// </summary>
    public class DocTipo
    {
        /// <summary>
        /// Tipo Documento
        /// </summary>
        public string tpd_id { get; set; }

        /// <summary>
        /// Subtipo, usado internamente pelo sistema
        /// </summary>
        public int tpd_subtipo { get; set; }

        /// <summary>
        /// Descrição do Tipo de Documento
        /// </summary>
        public string tpd_descricao { get; set; }
    }


    /// <summary>
    /// Modelo de Classe de Documento
    /// </summary>
    public class DocClasse
    {
        /// <summary>
        /// ID da Classe de Documento
        /// </summary>
        public int dcl_id { get; set; }

        /// <summary>
        /// Codigo da Classe
        /// </summary>
        public string dcl_codigo { get; set; }

        /// <summary>
        /// Descrição da Classe de Documento
        /// </summary>
        public string dcl_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int dcl_ativo { get; set; }

    }


    /// <summary>
    /// Modelo de Objeto do Documento selecionado 
    /// </summary>
    public class Documento_Objeto
    {
        /// <summary>
        /// Id do Documento selecionado
        /// </summary>
        public int doc_id { get; set; }

        /// <summary>
        /// Id do Objeto associado ao Documento selecionado
        /// </summary>
        public int obj_id { get; set; }

        /// <summary>
        /// Código do Objeto associado ao Documento selecionado
        /// </summary>
        public string obj_codigo { get; set; }


        /// <summary>
        /// Descrição do Objeto associado ao Documento selecionado
        /// </summary>
        public string obj_descricao { get; set; }

        /// <summary>
        /// Este Objeto está associado ao Documento selecionado?
        /// </summary>
        public int obj_Associado { get; set; }


    }



    /// <summary>
    /// Modelo de Ordem de Serviço do Documento selecionado 
    /// </summary>
    public class Documento_OrdemServico
    {
        /// <summary>
        /// Id do Documento selecionado
        /// </summary>
        public int doc_id { get; set; }

        /// <summary>
        /// Id da OS associada ao Documento selecionado
        /// </summary>
        public int ord_id { get; set; }

        /// <summary>
        /// Código da OS associada ao Documento selecionado
        /// </summary>
        public string ord_codigo { get; set; }


        /// <summary>
        /// Descrição da OS associada ao Documento selecionado
        /// </summary>
        public string ord_descricao { get; set; }

        /// <summary>
        /// Este OS está associada ao Documento selecionado?
        /// </summary>
        public int ord_Associada { get; set; }


    }

    /// <summary>
    /// Dados do Arquivo de anexo de documento
    /// </summary>
    public class ArquivoWeb
    {
        /// <summary>
        /// Caminho virtual de acesso
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Texto do link de acesso
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// Indicador de arquivo ou pasta
        /// </summary>
        public bool EhArquivo { get; set; }
    }

    /// <summary>
    /// Modelo dados adicionais de Arquivo para Upload
    /// </summary>
    public class Arquivo_Upload
    {
        /// <summary>
        /// Caminho do Servidor a ser salvo
        /// </summary>
        public string CaminhoServidor { get; set; }

        /// <summary>
        /// Arquivo
        /// </summary>
        public HttpPostedFileWrapper Arquivo { get; set; }

    }
    

}