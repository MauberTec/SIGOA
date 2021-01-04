CREATE TABLE [dbo].[tab_tpu_tipos_preco] (
    [tpt_id]               CHAR (1)       NOT NULL,
    [tpt_descricao]        NVARCHAR (255) NOT NULL,
    [tpt_ativo]            BIT            CONSTRAINT [DF_tab_tpu_tipo_preco_tpt_ativo] DEFAULT ((1)) NOT NULL,
    [tpt_deletado]         DATETIME       NULL,
    [tpt_data_criacao]     DATETIME       NOT NULL,
    [tpt_criado_por]       INT            NOT NULL,
    [tpt_data_atualizacao] DATETIME       NULL,
    [tpt_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_tpu_tipo] PRIMARY KEY CLUSTERED ([tpt_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tipo de tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_tipos_preco', @level2type = N'COLUMN', @level2name = N'tpt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_tipos_preco', @level2type = N'COLUMN', @level2name = N'tpt_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_tipos_preco', @level2type = N'COLUMN', @level2name = N'tpt_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_tipos_preco', @level2type = N'COLUMN', @level2name = N'tpt_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_tipos_preco', @level2type = N'COLUMN', @level2name = N'tpt_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_tipos_preco', @level2type = N'COLUMN', @level2name = N'tpt_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_tipos_preco', @level2type = N'COLUMN', @level2name = N'tpt_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_tipos_preco', @level2type = N'COLUMN', @level2name = N'tpt_atualizado_por';

