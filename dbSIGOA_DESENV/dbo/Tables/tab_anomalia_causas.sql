CREATE TABLE [dbo].[tab_anomalia_causas] (
    [aca_id]               INT            NOT NULL,
    [aca_codigo]           NVARCHAR (10)  NOT NULL,
    [aca_descricao]        NVARCHAR (255) NOT NULL,
    [aca_ativo]            BIT            CONSTRAINT [DF__causa__ATIVO__6B24EA82] DEFAULT ((1)) NULL,
    [aca_deletado]         DATETIME       NULL,
    [aca_data_criacao]     DATETIME       CONSTRAINT [DF__causa__CREATED__6C190EBB] DEFAULT (getdate()) NOT NULL,
    [aca_criado_por]       INT            NOT NULL,
    [aca_data_atualizacao] DATETIME       CONSTRAINT [DF__causa__UPDATED__6D0D32F4] DEFAULT (getdate()) NULL,
    [aca_atualizado_por]   INT            NULL,
    [leg_codigo]           NVARCHAR (10)  NOT NULL,
    [leg_id]               INT            NULL,
    CONSTRAINT [PK_aca_id] PRIMARY KEY CLUSTERED ([aca_id] ASC),
    CONSTRAINT [FK_tab_anomalia_causas_tab_anomalia_legendas] FOREIGN KEY ([leg_id]) REFERENCES [dbo].[tab_anomalia_legendas] ([leg_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'Descrição', @value = N'Tablea com as causas das anomalias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_causas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador da Causa da Anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_causas', @level2type = N'COLUMN', @level2name = N'aca_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código da Causa da Anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_causas', @level2type = N'COLUMN', @level2name = N'aca_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição da Causa da Anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_causas', @level2type = N'COLUMN', @level2name = N'aca_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_causas', @level2type = N'COLUMN', @level2name = N'aca_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_causas', @level2type = N'COLUMN', @level2name = N'aca_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_causas', @level2type = N'COLUMN', @level2name = N'aca_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_causas', @level2type = N'COLUMN', @level2name = N'aca_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_causas', @level2type = N'COLUMN', @level2name = N'aca_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_causas', @level2type = N'COLUMN', @level2name = N'aca_atualizado_por';

