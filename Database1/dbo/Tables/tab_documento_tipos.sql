CREATE TABLE [dbo].[tab_documento_tipos] (
    [tpd_id]               NVARCHAR (3)  NOT NULL,
    [tpd_subtipo]          INT           NOT NULL,
    [tpd_descricao]        VARCHAR (255) NOT NULL,
    [tpd_ativo]            BIT           CONSTRAINT [DF_tab_documento_tipos_tpd_ativo] DEFAULT ((1)) NOT NULL,
    [tpd_deletado]         DATETIME      NULL,
    [tpd_criado_por]       INT           CONSTRAINT [DF_tab_documento_tipos_tpd_criado_por] DEFAULT ((0)) NOT NULL,
    [tpd_data_criacao]     DATETIME      CONSTRAINT [DF_tab_documento_tipos_tpd_data_criacao] DEFAULT (getdate()) NOT NULL,
    [tpd_atualizado_por]   INT           NULL,
    [tpd_data_atualizacao] DATETIME      NULL,
    CONSTRAINT [PK_tab_documento_tipos_1] PRIMARY KEY CLUSTERED ([tpd_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador do tipo de documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_tipos', @level2type = N'COLUMN', @level2name = N'tpd_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'subtipo de documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_tipos', @level2type = N'COLUMN', @level2name = N'tpd_subtipo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_tipos', @level2type = N'COLUMN', @level2name = N'tpd_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_tipos', @level2type = N'COLUMN', @level2name = N'tpd_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_tipos', @level2type = N'COLUMN', @level2name = N'tpd_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_tipos', @level2type = N'COLUMN', @level2name = N'tpd_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_tipos', @level2type = N'COLUMN', @level2name = N'tpd_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_tipos', @level2type = N'COLUMN', @level2name = N'tpd_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_tipos', @level2type = N'COLUMN', @level2name = N'tpd_data_atualizacao';

