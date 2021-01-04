CREATE TABLE [dbo].[tab_documento_classes_projeto] (
    [dcl_id]               INT            NOT NULL,
    [dcl_codigo]           NVARCHAR (10)  NOT NULL,
    [dcl_descricao]        NVARCHAR (255) NOT NULL,
    [dcl_ativo]            BIT            NOT NULL,
    [dcl_deletado]         DATETIME       NULL,
    [dcl_criado_por]       INT            NOT NULL,
    [dcl_data_criacao]     DATETIME       NOT NULL,
    [dcl_atualizado_por]   INT            NULL,
    [dcl_data_atualizacao] DATETIME       NULL,
    CONSTRAINT [PK_tab_documento_classes] PRIMARY KEY CLUSTERED ([dcl_id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [idx_dcl_codigo]
    ON [dbo].[tab_documento_classes_projeto]([dcl_codigo] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de classe de projeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_classes_projeto', @level2type = N'COLUMN', @level2name = N'dcl_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código da classe de projeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_classes_projeto', @level2type = N'COLUMN', @level2name = N'dcl_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_classes_projeto', @level2type = N'COLUMN', @level2name = N'dcl_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_classes_projeto', @level2type = N'COLUMN', @level2name = N'dcl_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_classes_projeto', @level2type = N'COLUMN', @level2name = N'dcl_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_classes_projeto', @level2type = N'COLUMN', @level2name = N'dcl_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_classes_projeto', @level2type = N'COLUMN', @level2name = N'dcl_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_classes_projeto', @level2type = N'COLUMN', @level2name = N'dcl_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documento_classes_projeto', @level2type = N'COLUMN', @level2name = N'dcl_data_atualizacao';

