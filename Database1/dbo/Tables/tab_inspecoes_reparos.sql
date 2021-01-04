CREATE TABLE [dbo].[tab_inspecoes_reparos] (
    [ire_id]               INT            NOT NULL,
    [rpt_id]               INT            NOT NULL,
    [ian_id]               INT            NOT NULL,
    [tpu_id]               INT            NULL,
    [ire_quantidade_tpu]   REAL           NULL,
    [ire_descricao]        NVARCHAR (255) NOT NULL,
    [ire_ativo]            BIT            NULL,
    [ire_deletado]         DATETIME       NULL,
    [ire_data_criacao]     DATETIME       NOT NULL,
    [ire_criado_por]       INT            NOT NULL,
    [ire_data_atualizacao] DATETIME       NULL,
    [ire_atualizado_por]   INT            NULL,
    [ire_reparo_sugerido]  BIT            NOT NULL,
    CONSTRAINT [PK_ire_id] PRIMARY KEY CLUSTERED ([ire_id] ASC),
    CONSTRAINT [FK_tab_inspecoes_reparos_tab_inspecoes_anomalias] FOREIGN KEY ([ian_id]) REFERENCES [dbo].[tab_inspecoes_anomalias] ([ian_id]),
    CONSTRAINT [FK_tab_inspecoes_reparos_tab_reparo_tipos] FOREIGN KEY ([rpt_id]) REFERENCES [dbo].[tab_reparo_tipos] ([rpt_id]),
    CONSTRAINT [FK_tab_inspecoes_reparos_tab_tpu_precos_unitarios] FOREIGN KEY ([tpu_id]) REFERENCES [dbo].[tab_tpu_precos_unitarios] ([tpu_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de reparo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ire_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de tipo de reparo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'rpt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de Anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ian_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de preço unitário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'tpu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quantidade de TPU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ire_quantidade_tpu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ire_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ire_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ire_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ire_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ire_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ire_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ire_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica se o reparo é sugerido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_reparos', @level2type = N'COLUMN', @level2name = N'ire_reparo_sugerido';

