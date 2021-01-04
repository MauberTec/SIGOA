CREATE TABLE [dbo].[tab_anomalia_legendas] (
    [leg_id]               INT            NOT NULL,
    [leg_codigo]           NVARCHAR (10)  NOT NULL,
    [leg_descricao]        NVARCHAR (255) NOT NULL,
    [leg_ativo]            BIT            CONSTRAINT [DF__legenda__ATIVO__6B24EA82] DEFAULT ((1)) NULL,
    [leg_deletado]         DATETIME       NULL,
    [leg_data_criacao]     DATETIME       CONSTRAINT [DF__legenda__CREATED__6C190EBB] DEFAULT (getdate()) NULL,
    [leg_criado_por]       INT            NULL,
    [leg_data_atualizacao] DATETIME       CONSTRAINT [DF__legenda__UPDATED__6D0D32F4] DEFAULT (getdate()) NULL,
    [leg_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_leg_id] PRIMARY KEY CLUSTERED ([leg_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'Descrição', @value = N'Tabela de legenda de anomalias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_legendas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de legendas de anomalias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_legendas', @level2type = N'COLUMN', @level2name = N'leg_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de legendas de anomalias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_legendas', @level2type = N'COLUMN', @level2name = N'leg_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_legendas', @level2type = N'COLUMN', @level2name = N'leg_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_legendas', @level2type = N'COLUMN', @level2name = N'leg_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_legendas', @level2type = N'COLUMN', @level2name = N'leg_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_legendas', @level2type = N'COLUMN', @level2name = N'leg_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_legendas', @level2type = N'COLUMN', @level2name = N'leg_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_legendas', @level2type = N'COLUMN', @level2name = N'leg_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_legendas', @level2type = N'COLUMN', @level2name = N'leg_atualizado_por';

