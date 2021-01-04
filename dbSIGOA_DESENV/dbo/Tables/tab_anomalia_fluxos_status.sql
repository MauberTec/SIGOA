CREATE TABLE [dbo].[tab_anomalia_fluxos_status] (
    [fst_id]               INT            NOT NULL,
    [ast_id_de]            INT            NOT NULL,
    [ast_id_para]          INT            NOT NULL,
    [fst_descricao]        NVARCHAR (255) NULL,
    [fst_ativo]            BIT            CONSTRAINT [DF_tab_anom_fluxo_status2_fst_ativo] DEFAULT ((1)) NOT NULL,
    [fst_deletado]         DATETIME       NULL,
    [fst_data_criacao]     DATETIME       NOT NULL,
    [fst_criado_por]       INT            NOT NULL,
    [fst_data_atualizacao] DATETIME       NULL,
    [fst_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_anom_fluxo_status2] PRIMARY KEY CLUSTERED ([fst_id] ASC),
    CONSTRAINT [FK_tab_anom_fluxo_status_tab_anom_status] FOREIGN KEY ([ast_id_de]) REFERENCES [dbo].[tab_anomalia_status] ([ast_id]),
    CONSTRAINT [FK_tab_anom_fluxo_status_tab_anom_status1] FOREIGN KEY ([ast_id_para]) REFERENCES [dbo].[tab_anomalia_status] ([ast_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'Descrição', @value = N'Tabela que concatena os status das anomalias, fazendo seu fluxo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de fluxo de status de anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status', @level2type = N'COLUMN', @level2name = N'fst_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Status de anomalia inicial do fluxo de status', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status', @level2type = N'COLUMN', @level2name = N'ast_id_de';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Status de anomalia finall do fluxo de status', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status', @level2type = N'COLUMN', @level2name = N'ast_id_para';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição do fluxo de status de anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status', @level2type = N'COLUMN', @level2name = N'fst_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status', @level2type = N'COLUMN', @level2name = N'fst_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status', @level2type = N'COLUMN', @level2name = N'fst_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status', @level2type = N'COLUMN', @level2name = N'fst_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status', @level2type = N'COLUMN', @level2name = N'fst_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status', @level2type = N'COLUMN', @level2name = N'fst_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_fluxos_status', @level2type = N'COLUMN', @level2name = N'fst_atualizado_por';

