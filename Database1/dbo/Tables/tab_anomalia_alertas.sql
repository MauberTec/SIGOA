CREATE TABLE [dbo].[tab_anomalia_alertas] (
    [ale_id]               INT            NOT NULL,
    [ale_codigo]           NVARCHAR (10)  NOT NULL,
    [ale_descricao]        NVARCHAR (255) NOT NULL,
    [ale_ativo]            BIT            CONSTRAINT [DF__alerta__ATIVO__6B24EA82] DEFAULT ((1)) NULL,
    [ale_deletado]         DATETIME       NULL,
    [ale_data_criacao]     DATETIME       CONSTRAINT [DF__alerta__CREATED__6C190EBB] DEFAULT (getdate()) NOT NULL,
    [ale_criado_por]       INT            NOT NULL,
    [ale_data_atualizacao] DATETIME       CONSTRAINT [DF__alerta__UPDATED__6D0D32F4] DEFAULT (getdate()) NULL,
    [ale_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_ale_id] PRIMARY KEY CLUSTERED ([ale_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificados da anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_alertas', @level2type = N'COLUMN', @level2name = N'ale_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código da Anomalia - seguindo o manual de inspeções', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_alertas', @level2type = N'COLUMN', @level2name = N'ale_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição da anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_alertas', @level2type = N'COLUMN', @level2name = N'ale_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_alertas', @level2type = N'COLUMN', @level2name = N'ale_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_alertas', @level2type = N'COLUMN', @level2name = N'ale_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_alertas', @level2type = N'COLUMN', @level2name = N'ale_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_alertas', @level2type = N'COLUMN', @level2name = N'ale_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_alertas', @level2type = N'COLUMN', @level2name = N'ale_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_alertas', @level2type = N'COLUMN', @level2name = N'ale_atualizado_por';

