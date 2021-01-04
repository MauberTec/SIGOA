CREATE TABLE [dbo].[tab_tpu_maubertec] (
    [tmb_id]               INT            NOT NULL,
    [tmb_descricao]        NVARCHAR (255) NOT NULL,
    [tmb_valor]            REAL           NOT NULL,
    [tmb_data_tpu]         DATETIME       NOT NULL,
    [tmb_ativo]            BIT            NULL,
    [tmb_deletado]         DATETIME       NULL,
    [tmb_data_criacao]     DATETIME       NOT NULL,
    [tmb_criado_por]       INT            NOT NULL,
    [tmb_data_atualizacao] DATETIME       NULL,
    [tmb_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_tpu_maubertec] PRIMARY KEY CLUSTERED ([tmb_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de preço unitario Maubertec', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_maubertec', @level2type = N'COLUMN', @level2name = N'tmb_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_maubertec', @level2type = N'COLUMN', @level2name = N'tmb_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor do preço unitário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_maubertec', @level2type = N'COLUMN', @level2name = N'tmb_valor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data do preço unitário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_maubertec', @level2type = N'COLUMN', @level2name = N'tmb_data_tpu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_maubertec', @level2type = N'COLUMN', @level2name = N'tmb_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_maubertec', @level2type = N'COLUMN', @level2name = N'tmb_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_maubertec', @level2type = N'COLUMN', @level2name = N'tmb_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_maubertec', @level2type = N'COLUMN', @level2name = N'tmb_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_maubertec', @level2type = N'COLUMN', @level2name = N'tmb_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_maubertec', @level2type = N'COLUMN', @level2name = N'tmb_atualizado_por';

