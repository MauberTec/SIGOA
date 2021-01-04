CREATE TABLE [dbo].[tab_reparo_tipos] (
    [rpt_id]               INT            NOT NULL,
    [rpt_codigo]           NVARCHAR (10)  NOT NULL,
    [rpt_descricao]        NVARCHAR (255) NOT NULL,
    [rpt_ativo]            BIT            NULL,
    [rpt_deletado]         DATETIME       NULL,
    [rpt_data_criacao]     DATETIME       NOT NULL,
    [rpt_criado_por]       INT            NOT NULL,
    [rpt_data_atualizacao] DATETIME       NULL,
    [rpt_atualizado_por]   INT            NULL,
    [rpt_unidade]          NCHAR (10)     NULL,
    CONSTRAINT [PK_tab_rep_tipo] PRIMARY KEY CLUSTERED ([rpt_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tipo de reaparo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_tipos', @level2type = N'COLUMN', @level2name = N'rpt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código do tipo de reparo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_tipos', @level2type = N'COLUMN', @level2name = N'rpt_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_tipos', @level2type = N'COLUMN', @level2name = N'rpt_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_tipos', @level2type = N'COLUMN', @level2name = N'rpt_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_tipos', @level2type = N'COLUMN', @level2name = N'rpt_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_tipos', @level2type = N'COLUMN', @level2name = N'rpt_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_tipos', @level2type = N'COLUMN', @level2name = N'rpt_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_tipos', @level2type = N'COLUMN', @level2name = N'rpt_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_tipos', @level2type = N'COLUMN', @level2name = N'rpt_atualizado_por';

