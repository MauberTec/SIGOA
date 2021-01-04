CREATE TABLE [dbo].[tab_modulos] (
    [mod_id]               INT            NOT NULL,
    [mod_nome_modulo]      NVARCHAR (100) NOT NULL,
    [mod_descricao]        NVARCHAR (255) NOT NULL,
    [mod_ativo]            BIT            CONSTRAINT [DF__MODULO__ATIVO__74AE54BC] DEFAULT ((1)) NULL,
    [mod_deletado]         DATETIME       NULL,
    [mod_ordem]            INT            NULL,
    [mod_caminho]          NVARCHAR (255) NULL,
    [mod_pai_id]           INT            NULL,
    [mod_icone]            NVARCHAR (50)  NULL,
    [mod_data_criacao]     DATETIME       CONSTRAINT [DF__MODULO__CREATED__75A278F5] DEFAULT (getdate()) NULL,
    [mod_criado_por]       INT            NULL,
    [mod_data_atualizacao] DATETIME       CONSTRAINT [DF__MODULO__UPDATED__76969D2E] DEFAULT (getdate()) NULL,
    [mod_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_mod_id] PRIMARY KEY CLUSTERED ([mod_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador do módulo do sistema SIGOA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nome do módulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_nome_modulo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ordem de apresentação no MENU do sistema SIGOA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_ordem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caminho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_caminho';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modulo pai', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_pai_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ícone', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_icone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos', @level2type = N'COLUMN', @level2name = N'mod_atualizado_por';

