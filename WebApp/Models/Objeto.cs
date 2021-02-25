using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    /// <summary>
    /// Modelo de Objeto
    /// </summary>
    public class Objeto
    {
        /// <summary>
        /// Id do Objeto
        /// </summary>
        public int obj_id { get; set; }

        /// <summary>
        /// Classe do Objeto
        /// </summary>
        public int clo_id { get; set; }

        /// <summary>
        /// Tipo do Objeto
        /// </summary>
        public int tip_id { get; set; }

        /// <summary>
        /// Id do Pai do Objeto
        /// </summary>
        public int obj_pai { get; set; }

        /// <summary>
        /// Codigo do Objeto
        /// </summary>
        public string obj_codigo { get; set; }

        /// <summary>
        /// Descrição do Objeto
        /// </summary>
        public string obj_descricao { get; set; }


        /// <summary>
        /// Organização do Objeto
        /// </summary>
        public string obj_organizacao { get; set; }

        /// <summary>
        /// Departamento do Objeto
        /// </summary>
        public string obj_departamento { get; set; }

        /// <summary>
        /// Status do Objeto
        /// </summary>
        public string obj_status { get; set; }

        /// <summary>
        /// Arquivo kml
        /// </summary>
        public string obj_arquivo_kml { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int obj_ativo { get; set; }

        /// <summary>
        /// Posição no grid
        /// </summary>
        public double row_numero { get; set; }

        /// <summary>
        /// Flag de Profundidade
        /// </summary>
        public double row_expandida { get; set; }

        /// <summary>
        /// Profundidade
        /// </summary>
        public double nNivel { get; set; }


        /// <summary>
        /// Número de Filhos
        /// </summary>
        public int temFilhos { get; set; }

        /// <summary>
        /// Pode Deletar? - somente se não tiver O.S. ou Documentos associados
        /// </summary>
        public int obj_podeDeletar { get; set; }

        /// <summary>
        /// Nome da Classe de Objeto do ATRIBUTO
        /// </summary>
        public string clo_nome { get; set; }

        /// <summary>
        /// Nome do Tipo de Objeto do ATRIBUTO
        /// </summary>
        public string tip_nome { get; set; }

        /// <summary>  
        /// Lista de Classe de Objeto, para preenchimento de combo
        /// </summary>
        public List<SelectListItem> lstListacmbClassesObjeto { get; set; }

        /// <summary>  
        /// Lista de Tipos de Objeto, para preenchimento de combo
        /// </summary>
        public List<SelectListItem> lstTipos { get; set; }

    }

    // ********************************* TIPOS DE OBJETO **********************************************
    /// <summary>
    /// Modelo de TIPO de Objeto
    /// </summary>
    public class ObjTipo
    {
            
        /// <summary>
        /// Id do Tipo de Objeto
        /// </summary>
        public int tip_id { get; set; }

        /// <summary>
        /// Id da Classe vinculada ao Tipo de Objeto
        /// </summary>
        public int clo_id { get; set; }


        /// <summary>
        /// Codigo do Tipo de Objeto
        /// </summary>
        public string tip_codigo { get; set; }

        /// <summary>
        /// Nome do Tipo de Objeto
        /// </summary>
        public string tip_nome { get; set; }


        /// <summary>
        /// Descrição do Tipo de Objeto
        /// </summary>
        public string tip_descricao { get; set; }

        /// <summary>
        /// Mascara  de Codificacao do Objeto
        /// </summary>
        public string tip_mascara_codificacao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int tip_ativo { get; set; }

        /// <summary>
        /// Tem variável de inspecao?
        /// </summary>
        public int tem_var_inspecao { get; set; }


    }


    // ********************************* CLASSE DE OBJETO **********************************************
    /// <summary>
    /// Modelo de Classe Objeto
    /// </summary>
    public class ObjClasse
    {

        /// <summary>
        /// Id da Classe de Objeto
        /// </summary>
        public int clo_id { get; set; }

        /// <summary>
        /// Nome da Classe de Objeto
        /// </summary>
        public string clo_nome { get; set; }

        /// <summary>
        /// Descrição da Classe de Objeto
        /// </summary>
        public string clo_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int clo_ativo { get; set; }

    }


    // ********************************* ATRIBUTOS DE OBJETO **********************************************
    /// <summary>
    /// Modelo de ATRIBUTO de Objeto
    /// </summary>
    public class ObjAtributo
    {

        /// <summary>
        /// Id do ATRIBUTO de Objeto
        /// </summary>
        public int atr_id { get; set; }

        /// <summary>
        /// Id da Classe do ATRIBUTO
        /// </summary>
        public int clo_id { get; set; }

        /// <summary>
        /// Id do Tipo do ATRIBUTO
        /// </summary>
        public int tip_id { get; set; }


        /// <summary>
        /// Nome do ATRIBUTO
        /// </summary>
        public string atr_atributo_nome { get; set; }

        /// <summary>
        /// Descrição do ATRIBUTO
        /// </summary>
        public string atr_descricao { get; set; }

        /// <summary>
        /// Máscara de texto caso o item seja digitável, usado nas fichas de inspecao
        /// </summary>
        public string atr_mascara_texto { get; set; }

        /// <summary>
        /// Herdavel?
        /// </summary>
        public int atr_herdavel { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int atr_ativo { get; set; }

        /// <summary>
        /// É atributo funcional?
        /// </summary>
        public int atr_atributo_funcional { get; set; }

        /// <summary>
        /// Nome da Classe de Objeto do ATRIBUTO
        /// </summary>
        public string clo_nome { get; set; }

       /// <summary>
        /// Nome do Tipo de Objeto do ATRIBUTO
        /// </summary>
        public string tip_nome { get; set; }

       /// <summary>
        /// Lista Concatenada dos IDS dos Itens do Atributo
        /// </summary>
        public string atr_itens_ids { get; set; }

        /// <summary>
        /// Lista Concatenada dos Codigos dos Itens do Atributo
        /// </summary>
        public string atr_itens_codigo { get; set; }

        /// <summary>
        /// Lista Concatenada das Descrições dos Itens do Atributo
        /// </summary>
        public string atr_itens_descricao { get; set; }

        /// <summary>
        /// Apresentação dos itens (textbox, radio, checkbox)
        /// </summary>
        public string atr_apresentacao_itens { get; set; }

        /// <summary>  
        /// Lista de Classe de Objeto, para preenchimento de combo
        /// </summary>
        public List<SelectListItem> lstListacmbClassesObjeto { get; set; }

        /// <summary>  
        /// Lista de Tipos de Objeto, para preenchimento de combo
        /// </summary>
        public List<SelectListItem> lstTipos { get; set; }


}

    // ********************************* ITENS DE ATRIBUTO DE OBJETO **********************************************
    /// <summary>
    /// Modelo de ITENS DE ATRIBUTO  de Objeto
    /// </summary>
    public class ObjAtributoItem
    {
        /// <summary>
        /// Id do Item do ATRIBUTO
        /// </summary>
        public int ati_id { get; set; }

        /// <summary>
        /// Id do ATRIBUTO
        /// </summary>
        public int atr_id { get; set; }

        /// <summary>
        /// Item do Atributo de Objeto
        /// </summary>
        public string ati_item { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ati_ativo { get; set; }

        /// <summary>
        /// É atributo funcional?
        /// </summary>
        public int atr_atributo_funcional { get; set; }

    }

    // ********************************* VALORES DOS ATRIBUTOS DE OBJETO **********************************************
    /// <summary>
    /// Modelo de VALOR DE ATRIBUTO de Objeto
    /// </summary>
    public class ObjAtributoValores
    {

        /// <summary>
        /// Id do Objeto selecionado
        /// </summary>
        public int obj_id { get; set; }

        /// <summary>
        /// Id do ATRIBUTO de Objeto
        /// </summary>
        public int atr_id { get; set; }

        /// <summary>
        /// Id da Classe do Objeto
        /// </summary>
        public int clo_id { get; set; }

        /// <summary>
        /// Id do Tipo do Objeto
        /// </summary>
        public int tip_id { get; set; }

        /// <summary>
        /// Nome do atributo
        /// </summary>
        public string atr_atributo_nome { get; set; }

        /// <summary>
        /// Descricao do atributo
        /// </summary>
        public string atr_descricao { get; set; }

        /// <summary>
        /// Máscara de texto caso o item seja digitável, usado nas fichas de inspecao
        /// </summary>
        public string atr_mascara_texto { get; set; }

        /// <summary>
        /// Valor do atributo
        /// </summary>
        public string atv_valor { get; set; }

        /// <summary>
        /// Valor do atributo
        /// </summary>
        public string atv_valores { get; set; }

        /// <summary>
        /// Valor do atributo
        /// </summary>
        public string ati_ids { get; set; }

        /// <summary>
        /// Id de item de atributo
        /// </summary>
        public string ati_id { get; set; }

        /// <summary>
        /// Item de atributo
        /// </summary>
        public string ati_item { get; set; }


        /// <summary>
        /// Apresentação dos itens (textbox, radio, checkbox)
        /// </summary>
        public string atr_apresentacao_itens { get; set; }

        /// <summary>
        /// Todos os itens (se houver), separados por ";" 
        /// </summary>
        public string atr_itens_todos { get; set; }

        /// <summary>
        /// Todos as unidades (se houver), separadas por ";" 
        /// </summary>
        public string atr_unidades_todas { get; set; }


        /// <summary>
        /// Quantidade de itens do atributo
        /// </summary>
        public int nItens { get; set; }

        /// <summary>  
        /// Lista de Itens de Atributos, para preenchimento de combo
        /// </summary>
        public List<SelectListItem> lstItens { get; set; }

        /// <summary>  
        /// Lista de unidades de medida, para preenchimento de combo
        /// </summary>
        public List<SelectListItem> lstUnidadesMedida{ get; set; }

       /// <summary>
        /// Nome do controle da tabela 
        /// </summary>
        public string atv_controle { get; set; }

       /// <summary>
        /// aba de origem
        /// </summary>
        public string nome_aba { get; set; }

        /// <summary>
        /// EsquemaEstrutural
        /// </summary>
        public HttpPostedFileWrapper EsquemaEstrutural { get; set; }



    }



    // ********************************* GRUPOS E VALORES DE OBJETO **********************************************
    /// <summary>
    /// Valores dos Grupos e Variaveis
    /// </summary>
    public class GruposVariaveisValores
    {

        /// <summary>
        /// somente referencia para ordenacao
        /// </summary>
        public int numero { get; set; }

        /// <summary>
        /// nomeGrupo = coluna nome_grupo => somente para apresentar
        /// </summary>
        public string nomeGrupo { get; set; }

        /// <summary>
        /// mesclarLinhas, flag indicador de mesclagem de linhas na tabela 
        /// </summary>
        public int mesclarLinhas { get; set; }

        /// <summary>
        /// mesclarColunas, flag indicador de mesclagem de colunas para cabecalhos
        /// </summary>
        public int mesclarColunas { get; set; }

        /// <summary>
        /// cabecalho_Cor cor do cabeçalho
        /// </summary>
        public string cabecalho_Cor { get; set; }

        /// <summary>
        /// tip_pai do Grupo elecionado
        /// </summary>
        public int tip_pai { get; set; }

        /// <summary>
        /// Nome do pai do Grupo, indicador da posicao na hierarquia
        /// </summary>
        public string nome_pai { get; set; }

        /// <summary>
        /// Número/Posiçao do Cabeçalho de Grupo
        /// </summary>
        public int nCabecalhoGrupo { get; set; }

        /// <summary>
        /// Id do Objeto do nivel Grupo
        /// </summary>
        public int obj_id { get; set; }

        /// <summary>
        /// Objeto Tem Filhos ?
        /// </summary>
        public int TemFilhos { get; set; }

        /// <summary>
        /// Id do Tipo do Grupo
        /// </summary>
        public int tip_id_grupo { get; set; }

        /// <summary>
        /// Nome do Grupo
        /// </summary>
        public string nome_grupo { get; set; }

        /// <summary>
        /// Id da Variável do Grupo
        /// </summary>
        public int ogv_id { get; set; }

        /// <summary>
        /// Descricao da variavel
        /// </summary>
        public string variavel { get; set; }

        /// <summary>
        /// Id do valor da Caracterizacao da Situacao
        /// </summary>
        public int ogi_id_caracterizacao_situacao { get; set; }   
        
        /// <summary>
        /// Valor da Caracterizacao da Situacao
        /// </summary>
        public string ogi_id_caracterizacao_situacao_item { get; set; }  
        
        /// <summary>
        /// Id do valor da Condição da Inspeção
        /// </summary>
        public int ati_id_condicao_inspecao { get; set; }

        /// <summary>
        /// Valor da Condição da Inspeção
        /// </summary>
        public string ati_id_condicao_inspecao_item { get; set; }  

        /// <summary>
        /// Observacoes Gerais
        /// </summary>
        public string ovv_observacoes_gerais { get; set; }
      
        /// <summary>
        /// Id da TPU selecionada
        /// </summary>
        public int tpu_id { get; set; }

        /// <summary>
        /// Descricao da TPU selecionada
        /// </summary>
        public string tpu_descricao { get; set; }

        /// <summary>
        /// Id da unidade da TPU selecionada
        /// </summary>
        public int uni_id { get; set; }

        /// <summary>
        /// Descricao unidade da TPU selecionada
        /// </summary>
        public string uni_unidade { get; set; }   
            
        
        /// <summary>
        /// Quantidade sugerida
        /// </summary>
        public Double ovv_tpu_quantidade { get; set; }


        /// <summary>
        /// Lista dos itens de caracterização de situação, para preenchimento de combo
        /// </summary>
        public string caracterizacao_situacao_cmb { get; set; }   

        /// <summary>
        /// Lista dos itens de condição de inspeção, para preenchimento de combo
        /// </summary>
        public string condicao_inspecao_cmb { get; set; }

        /// <summary>
        /// Lista dos itens de tpu_descricao, para preenchimento de combo
        /// </summary>
        public string tpu_descricao_itens_cmb { get; set; }        


    }

    /// <summary>
    /// Valores das Conserrvas
    /// </summary>
    public class ConservaPolitica
    {
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public int ocp_id { get; set; }
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public int ogv_id { get; set; }
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public int ogi_id_caracterizacao_situacao { get; set; }
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public string ocp_descricao_alerta { get; set; }
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public string ocp_descricao_servico { get; set; }
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public bool ocp_ativo { get; set; }
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public DateTime ocp_deletado { get; set; }
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public DateTime ocp_data_criacao { get; set; }
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public int ocp_criado_por { get; set; }
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public DateTime ocp_data_atualizacao { get; set; }
        /// <summary>
        /// Campos da conserva
        /// </summary>
        public int ocp_atualizado_por { get; set; }
    }

    /// <summary>
    /// Priorizacao dos objetos
    /// </summary>
    public class ObjPriorizacao
    {
        /// <summary>
        /// Id da Priorização
        /// </summary>
       public int pri_id { get; set; }

        /// <summary>
        /// Id do Objeto ranqueado
        /// </summary>
       public int obj_id { get; set; }

        /// <summary>
        /// Ordem dos Objetos
        /// </summary>
       public int pri_ordem { get; set; }

        /// <summary>
        /// Classificação dos Objetos
        /// </summary>
       public int pri_classificacao { get; set; }

        /// <summary>
        /// Data da Classificação
        /// </summary>
       public string pri_data_classificacao { get; set; }

        /// <summary>
        /// Data da Inspeção do Objeto
        /// </summary>
       public string pri_data_inspecao { get; set; }

        /// <summary>
        /// Nota Final
        /// </summary>
       public  Double pri_nota_final { get; set; }

        /// <summary>
        /// Nota Estrutural
        /// </summary>
       public Double pri_nota_estrutura { get; set; }

        /// <summary>
        /// Nota Durabilidade
        /// </summary>
       public Double pri_nota_durabilidade { get; set; }

        /// <summary>
        /// Nota Ação
        /// </summary>
        public Double pri_nota_acao { get; set; }

        /// <summary>
        /// Descrição da Ação
        /// </summary>
        public string pri_acao { get; set; }


        /// <summary>
        /// Id do Status da Prioridade
        /// </summary>
        public string prs_id { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string pri_status { get; set; }

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string status_descricao { get; set; }


        /// <summary>
        /// Caso haja status, coloca cor de fundo
        /// </summary>
        public string corFundo { get; set; }


        // /// <summary>
        // /// Nota Funcionalidade
        // /// </summary>
        //public Double pri_nota_funcionalidade { get; set; }
        /// <summary>
        /// Nota da Importância da OAE
        /// </summary>
        public Double pri_nota_importancia_oae_malha { get; set; }

        /// <summary>
        /// Nota da VDM
        /// </summary>
       public Double pri_nota_vdm { get; set; }

        /// <summary>
        /// Nota da Principal Utilização
        /// </summary>
       public Double pri_nota_principal_utilizacao { get; set; }

        /// <summary>
        /// Nota da Facilidade de Desvio
        /// </summary>
       public Double pri_nota_facilidade_desvio { get; set; }

        /// <summary>
        /// Nota do Gabarito Vertical
        /// </summary>
       public Double pri_nota_gabarito_vertical { get; set; }

        /// <summary>
        /// Nota do Gabarito Horizontal
        /// </summary>
       public Double pri_nota_gabarito_horizontal { get; set; }

        /// <summary>
        /// Nota da Largura da Plataforma
        /// </summary>
       public Double pri_nota_largura_plataforma { get; set; }

        /// <summary>
        /// Nota da Agressividade Ambiental
        /// </summary>
       public Double pri_nota_agressividade_ambiental { get; set; }

        /// <summary>
        /// Nota do Trem Tipo
        /// </summary>
       public Double pri_nota_trem_tipo { get; set; }

        /// <summary>
        /// Nota da Barreira de Segurança
        /// </summary>
       public Double pri_nota_barreira_seguranca { get; set; }

        /// <summary>
        /// Nota da Restrição de Treminhões
        /// </summary>
       public Double pri_restricao_treminhoes { get; set; }
			
        /// <summary>
        /// Código do Objeto
        /// </summary>
       public string obj_codigo { get; set; }

        /// <summary>
        /// Descrição do Objeto
        /// </summary>
       public string obj_descricao { get; set; }


        /// <summary>
        /// Data da Inspeção
        /// </summary>
        public string ord_data_termino_execucao { get; set; }

        /// <summary>
        /// Descrição do Tipo de O.S.
        /// </summary>
        public string tos_descricao { get; set; }

    }



}