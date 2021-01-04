CREATE TABLE [dbo].[tab_reparo_politica] (
    [rpp_id]               INT           NOT NULL,
    [leg_codigo]           NVARCHAR (10) NOT NULL,
    [atp_codigo]           NVARCHAR (10) NOT NULL,
    [ale_codigo]           NVARCHAR (10) NOT NULL,
    [aca_id]               INT           NOT NULL,
    [rpt_id]               INT           NOT NULL,
    [rpp_ativo]            BIT           NULL,
    [rpp_deletado]         DATETIME      NULL,
    [rpp_data_criacao]     DATETIME      NOT NULL,
    [rpp_criado_por]       INT           NOT NULL,
    [rpp_data_atualizacao] DATETIME      NULL,
    [rpp_atualizado_por]   INT           NULL,
    [leg_id]               INT           NULL,
    [ale_id]               INT           NULL,
    [atp_id]               INT           NULL,
    CONSTRAINT [PK_tab_reparo_politica] PRIMARY KEY CLUSTERED ([rpp_id] ASC),
    CONSTRAINT [FK_tab_reparo_politica_tab_anomalia_alertas] FOREIGN KEY ([ale_id]) REFERENCES [dbo].[tab_anomalia_alertas] ([ale_id]),
    CONSTRAINT [FK_tab_reparo_politica_tab_reparo_tipos] FOREIGN KEY ([rpt_id]) REFERENCES [dbo].[tab_reparo_tipos] ([rpt_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de plitica de reparo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'rpp_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código da legenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'leg_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código do tipo da anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'atp_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código do alerta da anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'ale_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'chave estrangeira Identificador de causa de anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'aca_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'chave estrangeira Identificador de tipo de reparo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'rpt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'rpp_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'rpp_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'rpp_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'rpp_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'rpp_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_reparo_politica', @level2type = N'COLUMN', @level2name = N'rpp_atualizado_por';

