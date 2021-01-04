CREATE TABLE [dbo].[tab_contratos] (
    [con_id]               INT            NOT NULL,
    [con_codigo]           NVARCHAR (10)  NULL,
    [con_descricao]        NVARCHAR (255) NOT NULL,
    [con_ativo]            BIT            NULL,
    [con_deletado]         DATETIME       NULL,
    [con_data_criacao]     DATETIME       NOT NULL,
    [con_criado_por]       INT            NOT NULL,
    [con_data_atualizacao] DATETIME       NULL,
    [con_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_contratos] PRIMARY KEY CLUSTERED ([con_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_contratos', @level2type = N'COLUMN', @level2name = N'con_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código do contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_contratos', @level2type = N'COLUMN', @level2name = N'con_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_contratos', @level2type = N'COLUMN', @level2name = N'con_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_contratos', @level2type = N'COLUMN', @level2name = N'con_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_contratos', @level2type = N'COLUMN', @level2name = N'con_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_contratos', @level2type = N'COLUMN', @level2name = N'con_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_contratos', @level2type = N'COLUMN', @level2name = N'con_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_contratos', @level2type = N'COLUMN', @level2name = N'con_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_contratos', @level2type = N'COLUMN', @level2name = N'con_atualizado_por';

