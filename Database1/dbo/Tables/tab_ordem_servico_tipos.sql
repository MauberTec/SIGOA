CREATE TABLE [dbo].[tab_ordem_servico_tipos] (
    [tos_id]               INT            NOT NULL,
    [tos_codigo]           NVARCHAR (10)  NOT NULL,
    [tos_descricao]        NVARCHAR (255) NOT NULL,
    [tos_ativo]            BIT            NOT NULL,
    [tos_deletado]         DATETIME       NULL,
    [tos_data_criacao]     DATETIME       NOT NULL,
    [tos_criado_por]       INT            NOT NULL,
    [tos_data_atualizacao] DATETIME       NULL,
    [tos_atualizado_por]   INT            NULL,
    [tos_tipo_inspecao]    BIT            NULL,
    CONSTRAINT [PK_tab_os_tipos] PRIMARY KEY CLUSTERED ([tos_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Isentificador de Tipo de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_tipos', @level2type = N'COLUMN', @level2name = N'tos_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_tipos', @level2type = N'COLUMN', @level2name = N'tos_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_tipos', @level2type = N'COLUMN', @level2name = N'tos_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_tipos', @level2type = N'COLUMN', @level2name = N'tos_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_tipos', @level2type = N'COLUMN', @level2name = N'tos_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_tipos', @level2type = N'COLUMN', @level2name = N'tos_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_tipos', @level2type = N'COLUMN', @level2name = N'tos_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_tipos', @level2type = N'COLUMN', @level2name = N'tos_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_tipos', @level2type = N'COLUMN', @level2name = N'tos_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica se o tipo é utilizado também para tipo de inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_tipos', @level2type = N'COLUMN', @level2name = N'tos_tipo_inspecao';

