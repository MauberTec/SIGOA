CREATE TABLE [dbo].[tab_ordem_servico_orcamentos] (
    [orc_id]               INT            NOT NULL,
    [orc_limite]           REAL           NULL,
    [orc_descricao]        NVARCHAR (255) NOT NULL,
    [orc_ativo]            BIT            NOT NULL,
    [orc_deletado]         DATETIME       NULL,
    [orc_data_criacao]     DATETIME       NOT NULL,
    [orc_criado_por]       INT            NULL,
    [orc_data_atualizacao] DATETIME       NULL,
    [orc_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_ordem_servico_orcamentos] PRIMARY KEY CLUSTERED ([orc_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de orçamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos', @level2type = N'COLUMN', @level2name = N'orc_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira tipo de orçamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos', @level2type = N'COLUMN', @level2name = N'orc_limite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos', @level2type = N'COLUMN', @level2name = N'orc_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos', @level2type = N'COLUMN', @level2name = N'orc_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos', @level2type = N'COLUMN', @level2name = N'orc_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos', @level2type = N'COLUMN', @level2name = N'orc_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos', @level2type = N'COLUMN', @level2name = N'orc_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos', @level2type = N'COLUMN', @level2name = N'orc_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos', @level2type = N'COLUMN', @level2name = N'orc_atualizado_por';

