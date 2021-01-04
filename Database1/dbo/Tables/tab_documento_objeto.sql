CREATE TABLE [dbo].[tab_documento_objeto] (
    [dob_id]               INT      IDENTITY (1, 1) NOT NULL,
    [doc_id]               BIGINT   NOT NULL,
    [obj_id]               BIGINT   NOT NULL,
    [dob_deletado]         DATETIME NULL,
    [dob_data_criacao]     DATETIME NOT NULL,
    [dob_criado_por]       INT      NOT NULL,
    [dob_data_atualizacao] DATETIME NULL,
    [dob_atualizado_por]   INT      NULL,
    CONSTRAINT [PK_tab_documento_objeto] PRIMARY KEY CLUSTERED ([dob_id] ASC),
    CONSTRAINT [FK_tab_documento_objeto_tab_documentos] FOREIGN KEY ([doc_id]) REFERENCES [dbo].[tab_documentos] ([doc_id]),
    CONSTRAINT [FK_tab_documento_objeto_tab_objetos] FOREIGN KEY ([obj_id]) REFERENCES [dbo].[tab_objetos] ([obj_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador da tabela que relaciona documento a objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_objeto', @level2type = N'COLUMN', @level2name = N'dob_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador do documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_objeto', @level2type = N'COLUMN', @level2name = N'doc_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador do objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_objeto', @level2type = N'COLUMN', @level2name = N'obj_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_objeto', @level2type = N'COLUMN', @level2name = N'dob_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_objeto', @level2type = N'COLUMN', @level2name = N'dob_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_objeto', @level2type = N'COLUMN', @level2name = N'dob_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_objeto', @level2type = N'COLUMN', @level2name = N'dob_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_objeto', @level2type = N'COLUMN', @level2name = N'dob_atualizado_por';

