CREATE TABLE [dbo].[tab_tpu_fases] (
    [fas_id]               INT            NOT NULL,
    [fas_descricao]        NVARCHAR (255) NOT NULL,
    [fas_ativo]            BIT            CONSTRAINT [DF_tab_fase_tpu_faz_ativo] DEFAULT ((1)) NULL,
    [fas_deletado]         DATETIME       NULL,
    [fas_data_criacao]     DATETIME       CONSTRAINT [DF_tab_fase_tpu_faz_data_criacao] DEFAULT (getdate()) NOT NULL,
    [fas_criado_por]       INT            CONSTRAINT [DF_tab_fase_tpu_faz_criado_por] DEFAULT ((0)) NOT NULL,
    [fas_data_atualizacao] DATETIME       NULL,
    [fas_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_fase_tpu] PRIMARY KEY CLUSTERED ([fas_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de fase de TPU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_fases', @level2type = N'COLUMN', @level2name = N'fas_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_fases', @level2type = N'COLUMN', @level2name = N'fas_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_fases', @level2type = N'COLUMN', @level2name = N'fas_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_fases', @level2type = N'COLUMN', @level2name = N'fas_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_fases', @level2type = N'COLUMN', @level2name = N'fas_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_fases', @level2type = N'COLUMN', @level2name = N'fas_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_fases', @level2type = N'COLUMN', @level2name = N'fas_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_fases', @level2type = N'COLUMN', @level2name = N'fas_atualizado_por';

