using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{

    // ************* LEGENDA  ***************************************

    /// <summary>
    /// Modelo de Legenda de Anomalia
    /// </summary>
    public class AnomLegenda
    {
        /// <summary>
        /// Id da Legenda
        /// </summary>
        public int leg_id { get; set; }

        /// <summary>
        /// Código da Legenda
        /// </summary>
        public string leg_codigo { get; set; }

        /// <summary>
        /// Descrição da Legenda
        /// </summary>
        public string leg_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int leg_ativo { get; set; }

    }

    // ************* TIPO  ***************************************

    /// <summary>
    /// Modelo de Tipo de Anomalia
    /// </summary>
    public class AnomTipo
    {
        /// <summary>
        /// Id do Tipo
        /// </summary>
        public int atp_id { get; set; }

        /// <summary>
        /// Código do Tipo
        /// </summary>
        public string atp_codigo { get; set; }

        /// <summary>
        /// Descrição do Tipo
        /// </summary>
        public string atp_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int atp_ativo { get; set; }

    }

    // ************* CAUSA  ***************************************

    /// <summary>
    /// Modelo de Causa de Anomalia
    /// </summary>
    public class AnomCausa
    {
        /// <summary>
        /// Id da Causa
        /// </summary>
        public int aca_id { get; set; }

        /// <summary>
        /// Id da Legenda
        /// </summary>
        public int leg_id { get; set; }

        /// <summary>
        /// Código da Legenda
        /// </summary>
        public string leg_codigo { get; set; }

        /// <summary>
        /// Descrição da Legenda
        /// </summary>
        public string leg_descricao { get; set; }



        /// <summary>
        /// Código da Causa
        /// </summary>
        public string aca_codigo { get; set; }

        /// <summary>
        /// Descrição da Causa
        /// </summary>
        public string aca_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int aca_ativo { get; set; }

    }

    // ************* ALERTA  ***************************************

    /// <summary>
    /// Modelo de Alerta de Anomalia
    /// </summary>
    public class AnomAlerta
    {
        /// <summary>
        /// Id do Alerta
        /// </summary>
        public int ale_id { get; set; }

        /// <summary>
        /// Código do Alerta
        /// </summary>
        public string ale_codigo { get; set; }

        /// <summary>
        /// Descrição do Alerta
        /// </summary>
        public string ale_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ale_ativo { get; set; }

    }


    // *************  STATUS  ***************************************
    /// <summary>
    /// Modelo de Status de Anomalia
    /// </summary>
    public class AnomStatus
    {
        /// <summary>
        /// Id do Status
        /// </summary>
        public int ast_id { get; set; }

        /// <summary>
        /// Código do Status
        /// </summary>
        public string ast_codigo { get; set; }

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string ast_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ast_ativo { get; set; }

    }



    // ************* FLUXO DE STATUS  ***************************************
    /// <summary>
    /// Fluxo de Status
    /// </summary>
    public class AnomFluxoStatus
    {

        /// <summary>
        /// Id do Fluxo de Status 
        /// </summary>
        public int fst_id { get; set; }

        /// <summary>
        /// Id do Status Origem 
        /// </summary>
        public int ast_id_de { get; set; }

        /// <summary>
        /// Codigo do Status Origem  
        /// </summary>
        public string ast_de_codigo { get; set; }

        /// <summary>
        /// Descrição do Status Origem 
        /// </summary>
        public string ast_de_descricao { get; set; }


        /// <summary>
        /// Id do Status Destino 
        /// </summary>
        public int ast_id_para { get; set; }

        /// <summary>
        /// Codigo do Status Destino  
        /// </summary>
        public string ast_para_codigo { get; set; }

        /// <summary>
        /// Descrição do Status Destino 
        /// </summary>
        public string ast_para_descricao { get; set; }

        /// <summary>
        /// Descrição do Fluxo do Status 
        /// </summary>
        public string fst_descricao { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int fst_ativo { get; set; }

    }


    // ************* InspecaoAnomalia  ***************************************
    /// <summary>
    /// InspecaoAnomalia
    /// </summary>
    public class InspecaoAnomalia
    {
                                  //   

        /// <summary>
        /// Número da linha, para manter a ordenação 
        /// </summary>
        public int rownum { get; set; }

        /// <summary>
        /// Id do Objeto 
        /// </summary>
        public int obj_id { get; set; }

        /// <summary>
        /// Descrição do Objeto  
        /// </summary>
        public string obj_descricao { get; set; }

        /// <summary>
        /// Id do Objeto pai 
        /// </summary>
        public int obj_pai { get; set; }

        /// <summary>
        /// Codigo do Objeto  
        /// </summary>
        public string obj_codigo { get; set; }

        /// <summary>
        /// Nivel de profundidade hierárquica do objeto 
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// Numero do Item  
        /// </summary>
        public string item { get; set; }

        /// <summary>
        /// Id da classe do Objeto 
        /// </summary>
        public int clo_id { get; set; }

        /// <summary>
        /// Classe do Objeto  
        /// </summary>
        public string clo_nome { get; set; }

        /// <summary>
        /// Id do Tipo do Objeto 
        /// </summary>
        public int tip_id { get; set; }

        /// <summary>
        /// Tipo do Objeto  
        /// </summary>
        public string tip_nome { get; set; }

        /// <summary>
        /// Id da Anomalia 
        /// </summary>
        public int ian_id { get; set; }

        /// <summary>
        /// Id da Inspecao 
        /// </summary>
        public int ins_id { get; set; }

        /// <summary>
        /// Número da Anomalia 
        /// </summary>
        public string ian_numero { get; set; }

        /// <summary>
        /// Id do tipo de anomalia 
        /// </summary>
        public int atp_id { get; set; }

        /// <summary>
        /// Sigla da anomalia  
        /// </summary>
        public string ian_sigla { get; set; }

        /// <summary>
        /// Valor da Coluna Localizacao da anomalia 
        /// </summary>
        public string col_Localizacao { get; set; }


        /// <summary>
        /// Id do nivel de alerta 
        /// </summary>
        public int ale_id { get; set; }

        /// <summary>
        /// Quantidade de anomalia 
        /// </summary>
        public int ian_quantidade { get; set; }

        /// <summary>
        /// Espaçamento entre anomalia em cm
        /// </summary>
        public int ian_espacamento { get; set; }

        /// <summary>
        /// Largura de anomalia em cm
        /// </summary>
        public int ian_largura { get; set; }

        /// <summary>
        /// Comprimento de anomalia em cm
        /// </summary>
        public int ian_comprimento { get; set; }

        /// <summary>
        /// Abertura mímima da anomalia em mm
        /// </summary>
        public int ian_abertura_minima { get; set; }

        /// <summary>
        /// Abertura máxima da anomalia em mm
        /// </summary>
        public int ian_abertura_maxima { get; set; }

        /// <summary>
        /// Id de causa provável
        /// </summary>
        public int aca_id { get; set; }

        /// <summary>
        /// Número da fotografia
        /// </summary>
        public string ian_fotografia { get; set; }

        /// <summary>
        /// Número do croqui
        /// </summary>
        public string ian_croqui { get; set; }

        /// <summary>
        /// Desenho 
        /// </summary>
        public string ian_desenho { get; set; }

        /// <summary>
        /// Observações 
        /// </summary>
        public string ian_observacoes { get; set; }

        /// <summary>
        /// Id da legenda
        /// </summary>
        public int leg_id { get; set; }

        /// <summary>
        /// Ativo?
        /// </summary>
        public int ian_ativo { get; set; }


        /// <summary>
        /// Id do reparo sugerido
        /// </summary>
        public int rpt_id_sugerido { get; set; }

        /// <summary>
        /// Codigo do reparo sugerido
        /// </summary>
        public string rpt_id_sugerido_codigo { get; set; }

        /// <summary>
        /// Descricao do reparo sugerido
        /// </summary>
        public string rpt_id_sugerido_descricao { get; set; }

        /// <summary>
        /// Unidade da Quantidade do reparo sugerido
        /// </summary>
        public string rpt_id_sugerido_unidade { get; set; }

       /// <summary>
        /// Quantidade do reparo sugerido
        /// </summary>
        public decimal ian_quantidade_sugerida { get; set; }


        /// <summary>
        /// Id do reparo adotado
        /// </summary>
        public int rpt_id_adotado { get; set; }

        /// <summary>
        /// Codigo do reparo adotado
        /// </summary>
        public string rpt_id_adotado_codigo { get; set; }

        /// <summary>
        /// Descricao do reparo adotado
        /// </summary>
        public string rpt_id_adotado_descricao { get; set; }

        /// <summary>
        /// Unidade da Quantidade do reparo adotada
        /// </summary>
        public string rpt_id_adotado_unidade { get; set; }
 
        /// <summary>
        /// Quantidade do reparo adotado
        /// </summary>
        public decimal ian_quantidade_adotada { get; set; }



        /// <summary>
        /// Código do tipo de anomalia
        /// </summary>
        public string atp_codigo { get; set; }

        /// <summary>
        /// Descrição do tipo de anomalia
        /// </summary>
        public string atp_descricao { get; set; }

        /// <summary>
        /// Código do alerta da anomalia
        /// </summary>
        public string ale_codigo { get; set; }

        /// <summary>
        /// Descrição do Alerta de anomalia
        /// </summary>
        public string ale_descricao { get; set; }

        /// <summary>
        /// Código da causa da anomalia
        /// </summary>
        public string aca_codigo { get; set; }

        /// <summary>
        /// Descrição da causa da anomalia
        /// </summary>
        public string aca_descricao { get; set; }


        /// <summary>
        /// Código da legenda da anomalia
        /// </summary>
        public string leg_codigo { get; set; }

        /// <summary>
        /// Descrição da legenda da anomalia
        /// </summary>
        public string leg_descricao { get; set; }


        /// <summary>
        /// Lista de legendas para preenchimento de combo
        /// </summary>
        public string lstLegendas { get; set; }

        /// <summary>
        /// Lista de alertas para preenchimento de combo
        /// </summary>
        public string lstAlertas { get; set; }

        /// <summary>
        /// Lista de tipos de anomalia para preenchimento de combo
        /// </summary>
        public string lstTipos { get; set; }

        /// <summary>
        /// Lista de causas para preenchimento de combo
        /// </summary>
        public string lstCausas { get; set; }

        /// <summary>
        /// Lista de Tipos de Reparo para preenchimento de combo
        /// </summary>
        public string lstReparoTipos { get; set; }

        /// <summary>
        /// Responsavel pela Inspeção
        /// </summary>
        public string ins_anom_Responsavel { get; set; }

        /// <summary>
        /// Codigo do Objeto TipoOAE
        /// </summary>
        public string obj_codigo_TipoOAE { get; set; }

        /// <summary>
        /// Data da Inspeção
        /// </summary>
        public string ins_anom_data { get; set; }

        /// <summary>
        /// Resposta do Quadro A (Sim/Não)
        /// </summary>
        public string ins_anom_quadroA_1 { get; set; }

        /// <summary>
        /// Resposta do Quadro A (itens)
        /// </summary>
        public string ins_anom_quadroA_2 { get; set; }




    }




}