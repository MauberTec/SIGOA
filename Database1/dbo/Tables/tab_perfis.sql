CREATE TABLE [dbo].[tab_perfis] (
    [per_id]               INT            NOT NULL,
    [per_descricao]        NVARCHAR (255) NOT NULL,
    [per_ativo]            BIT            CONSTRAINT [DF__PERFIL__ATIVO__66603565] DEFAULT ((1)) NULL,
    [per_deletado]         DATETIME       NULL,
    [per_data_criacao]     DATETIME       CONSTRAINT [DF__PERFIL__CREATED__02084FDA] DEFAULT (getdate()) NULL,
    [per_criado_por]       INT            NULL,
    [per_data_atualizacao] DATETIME       CONSTRAINT [DF__PERFIL__UPDATED__02FC7413] DEFAULT (getdate()) NULL,
    [per_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_pfl_id] PRIMARY KEY CLUSTERED ([per_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Perfil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis', @level2type = N'COLUMN', @level2name = N'per_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição de Perfil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis', @level2type = N'COLUMN', @level2name = N'per_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis', @level2type = N'COLUMN', @level2name = N'per_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis', @level2type = N'COLUMN', @level2name = N'per_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis', @level2type = N'COLUMN', @level2name = N'per_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis', @level2type = N'COLUMN', @level2name = N'per_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis', @level2type = N'COLUMN', @level2name = N'per_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis', @level2type = N'COLUMN', @level2name = N'per_atualizado_por';

