CREATE TABLE [dbo].[tab_documento_ordens_servicos] (
    [dos_id]               INT      IDENTITY (1, 1) NOT NULL,
    [doc_id]               BIGINT   NOT NULL,
    [ord_id]               BIGINT   NOT NULL,
    [dos_deletado]         DATETIME NULL,
    [dos_data_criacao]     DATETIME CONSTRAINT [DF_tab_documento_os_dos_data_criacao] DEFAULT (getdate()) NOT NULL,
    [dos_criado_por]       INT      CONSTRAINT [DF_tab_documento_os_dos_criado_por] DEFAULT ((1)) NOT NULL,
    [dos_data_atualizacao] DATETIME NULL,
    [dos_atualizado_por]   INT      NULL,
    [dos_referencia]       BIT      NULL,
    CONSTRAINT [PK_tab_documento_os] PRIMARY KEY CLUSTERED ([dos_id] ASC),
    CONSTRAINT [FK_tab_documento_os_tab_documentos] FOREIGN KEY ([doc_id]) REFERENCES [dbo].[tab_documentos] ([doc_id]),
    CONSTRAINT [FK_tab_documento_os_tab_ordem_servico] FOREIGN KEY ([ord_id]) REFERENCES [dbo].[tab_ordens_servico] ([ord_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador da tabela que relaciona documento a OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_ordens_servicos', @level2type = N'COLUMN', @level2name = N'dos_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_ordens_servicos', @level2type = N'COLUMN', @level2name = N'doc_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_ordens_servicos', @level2type = N'COLUMN', @level2name = N'ord_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_ordens_servicos', @level2type = N'COLUMN', @level2name = N'dos_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data de criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_ordens_servicos', @level2type = N'COLUMN', @level2name = N'dos_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_ordens_servicos', @level2type = N'COLUMN', @level2name = N'dos_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_ordens_servicos', @level2type = N'COLUMN', @level2name = N'dos_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_ordens_servicos', @level2type = N'COLUMN', @level2name = N'dos_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica caso valor 1 se o documento foi levado como referência na OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_ordens_servicos', @level2type = N'COLUMN', @level2name = N'dos_referencia';

