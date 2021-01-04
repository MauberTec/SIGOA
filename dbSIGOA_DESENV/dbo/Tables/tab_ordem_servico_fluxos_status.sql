CREATE TABLE [dbo].[tab_ordem_servico_fluxos_status] (
    [fos_id]               INT            NOT NULL,
    [sos_id_de]            INT            NOT NULL,
    [sos_id_para]          INT            NOT NULL,
    [fos_descricao]        NVARCHAR (255) NULL,
    [fos_ativo]            BIT            CONSTRAINT [DF_tab_os_fluxo_status2_fos_ativo] DEFAULT ((1)) NOT NULL,
    [fos_deletado]         DATETIME       NULL,
    [fos_data_criacao]     DATETIME       NOT NULL,
    [fos_criado_por]       INT            NOT NULL,
    [fos_data_atualizacao] DATETIME       NULL,
    [fos_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_os_fluxo_status2] PRIMARY KEY CLUSTERED ([fos_id] ASC),
    CONSTRAINT [FK_tab_os_fluxo_status_tab_os_status] FOREIGN KEY ([sos_id_de]) REFERENCES [dbo].[tab_ordem_servico_status] ([sos_id]),
    CONSTRAINT [FK_tab_os_fluxo_status_tab_os_status1] FOREIGN KEY ([sos_id_para]) REFERENCES [dbo].[tab_ordem_servico_status] ([sos_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de fluxo de status de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_fluxos_status', @level2type = N'COLUMN', @level2name = N'fos_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Status de Origem do fluxo de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_fluxos_status', @level2type = N'COLUMN', @level2name = N'sos_id_de';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Status de final de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_fluxos_status', @level2type = N'COLUMN', @level2name = N'sos_id_para';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_fluxos_status', @level2type = N'COLUMN', @level2name = N'fos_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_fluxos_status', @level2type = N'COLUMN', @level2name = N'fos_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_fluxos_status', @level2type = N'COLUMN', @level2name = N'fos_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_fluxos_status', @level2type = N'COLUMN', @level2name = N'fos_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_fluxos_status', @level2type = N'COLUMN', @level2name = N'fos_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_fluxos_status', @level2type = N'COLUMN', @level2name = N'fos_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_fluxos_status', @level2type = N'COLUMN', @level2name = N'fos_atualizado_por';

