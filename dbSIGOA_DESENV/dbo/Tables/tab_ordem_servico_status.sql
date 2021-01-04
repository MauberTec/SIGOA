CREATE TABLE [dbo].[tab_ordem_servico_status] (
    [sos_id]               INT            NOT NULL,
    [sos_codigo]           NVARCHAR (10)  NOT NULL,
    [sos_descricao]        NVARCHAR (255) NOT NULL,
    [sos_ativo]            BIT            CONSTRAINT [DF_tab_os_status_sos_ativo] DEFAULT ((1)) NOT NULL,
    [sos_deletado]         DATETIME       NULL,
    [sos_data_criacao]     DATETIME       NOT NULL,
    [sos_criado_por]       INT            NOT NULL,
    [sos_data_atualizacao] DATETIME       NULL,
    [sos_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_os_status] PRIMARY KEY CLUSTERED ([sos_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de status de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_status', @level2type = N'COLUMN', @level2name = N'sos_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de status de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_status', @level2type = N'COLUMN', @level2name = N'sos_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_status', @level2type = N'COLUMN', @level2name = N'sos_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_status', @level2type = N'COLUMN', @level2name = N'sos_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_status', @level2type = N'COLUMN', @level2name = N'sos_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_status', @level2type = N'COLUMN', @level2name = N'sos_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_status', @level2type = N'COLUMN', @level2name = N'sos_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_status', @level2type = N'COLUMN', @level2name = N'sos_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_status', @level2type = N'COLUMN', @level2name = N'sos_atualizado_por';

