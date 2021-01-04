CREATE TABLE [dbo].[tab_inspecao_tipos] (
    [ipt_id]               INT            NOT NULL,
    [ipt_codigo]           NVARCHAR (10)  NOT NULL,
    [ipt_descricao]        NVARCHAR (255) NOT NULL,
    [ipt_ativo]            BIT            NULL,
    [ipt_deletado]         DATETIME       NULL,
    [ipt_data_criacao]     DATETIME       NOT NULL,
    [ipt_criado_por]       INT            NOT NULL,
    [ipt_data_atualizacao] DATETIME       NULL,
    [ipt_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_inspecao_tipo] PRIMARY KEY CLUSTERED ([ipt_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tipo de inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_tipos', @level2type = N'COLUMN', @level2name = N'ipt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código mnemónico do tipo de inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_tipos', @level2type = N'COLUMN', @level2name = N'ipt_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_tipos', @level2type = N'COLUMN', @level2name = N'ipt_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_tipos', @level2type = N'COLUMN', @level2name = N'ipt_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_tipos', @level2type = N'COLUMN', @level2name = N'ipt_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_tipos', @level2type = N'COLUMN', @level2name = N'ipt_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_tipos', @level2type = N'COLUMN', @level2name = N'ipt_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_tipos', @level2type = N'COLUMN', @level2name = N'ipt_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_tipos', @level2type = N'COLUMN', @level2name = N'ipt_atualizado_por';

