CREATE TABLE [dbo].[tab_anomalia_status] (
    [ast_id]               INT            NOT NULL,
    [ast_codigo]           NVARCHAR (10)  NOT NULL,
    [ast_descricao]        NVARCHAR (255) NOT NULL,
    [ast_ativo]            BIT            CONSTRAINT [DF__status__ATIVO__6B24EA82] DEFAULT ((1)) NULL,
    [ast_deletado]         DATETIME       NULL,
    [ast_data_criacao]     DATETIME       CONSTRAINT [DF__status__CREATED__6C190EBB] DEFAULT (getdate()) NOT NULL,
    [ast_criado_por]       INT            NOT NULL,
    [ast_data_atualizacao] DATETIME       CONSTRAINT [DF__status__UPDATED__6D0D32F4] DEFAULT (getdate()) NULL,
    [ast_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_ast_id] PRIMARY KEY CLUSTERED ([ast_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'Descrição', @value = N'Tabela com os Status de anomalias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de status de anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_status', @level2type = N'COLUMN', @level2name = N'ast_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de status de anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_status', @level2type = N'COLUMN', @level2name = N'ast_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_status', @level2type = N'COLUMN', @level2name = N'ast_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_status', @level2type = N'COLUMN', @level2name = N'ast_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_status', @level2type = N'COLUMN', @level2name = N'ast_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_status', @level2type = N'COLUMN', @level2name = N'ast_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_status', @level2type = N'COLUMN', @level2name = N'ast_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_status', @level2type = N'COLUMN', @level2name = N'ast_atualizado_por';

