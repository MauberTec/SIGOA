CREATE TABLE [dbo].[tab_notificacao_ocorrencias] (
    [noc_id]                           BIGINT          NOT NULL,
    [ord_id]                           BIGINT          NOT NULL,
    [noc_data_notificacao]             NVARCHAR (25)   NULL,
    [noc_responsavel_notificacao]      NVARCHAR (100)  NULL,
    [noc_descricao_ocorrencia]         NVARCHAR (1000) NULL,
    [noc_solicitante]                  NVARCHAR (100)  NULL,
    [noc_solicitante_data]             NVARCHAR (25)   NULL,
    [noc_responsavel_recebimento]      NVARCHAR (100)  NULL,
    [noc_responsavel_recebimento_data] NVARCHAR (25)   NULL,
    [noc_ativo]                        BIT             NOT NULL,
    [noc_deletado]                     DATETIME        NULL,
    [noc_data_criacao]                 DATETIME        NOT NULL,
    [noc_criado_por]                   INT             NOT NULL,
    [noc_data_atualizacao]             DATETIME        NULL,
    [noc_atualizado_por]               INT             NULL,
    CONSTRAINT [PK_tab_notificacao_ocorrencias] PRIMARY KEY CLUSTERED ([noc_id] ASC)
);

