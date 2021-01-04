CREATE TABLE [dbo].[tab_inspecoes_anomalias] (
    [ian_id]                  INT            NOT NULL,
    [obj_id]                  BIGINT         NOT NULL,
    [ins_id]                  BIGINT         NOT NULL,
    [ian_numero]              INT            NOT NULL,
    [atp_id]                  INT            NOT NULL,
    [ian_sigla]               NVARCHAR (2)   NOT NULL,
    [ale_id]                  INT            NOT NULL,
    [ian_quantidade]          INT            NOT NULL,
    [ian_espacamento]         INT            NOT NULL,
    [ian_largura]             INT            NOT NULL,
    [ian_comprimento]         INT            NOT NULL,
    [ian_abertura_minima]     INT            NOT NULL,
    [ian_abertura_maxima]     INT            NOT NULL,
    [aca_id]                  INT            NOT NULL,
    [ian_fotografia]          VARCHAR (255)  NULL,
    [ian_desenho]             VARCHAR (255)  NULL,
    [ian_observacoes]         NVARCHAR (255) NULL,
    [leg_id]                  INT            NOT NULL,
    [ian_ativo]               BIT            NULL,
    [ian_deletado]            DATETIME       NULL,
    [ian_data_criacao]        DATETIME       NULL,
    [ian_criado_por]          INT            NULL,
    [ian_data_atualizacao]    DATETIME       NULL,
    [ian_atualizado_por]      INT            NULL,
    [ian_croqui]              VARCHAR (255)  NULL,
    [ian_quantidade_adotada]  REAL           NULL,
    [ian_quantidade_sugerida] REAL           NULL,
    [rpt_id_adotado]          INT            NULL,
    [rpt_id_sugerido]         INT            NULL,
    CONSTRAINT [PK_ian_id] PRIMARY KEY CLUSTERED ([ian_id] ASC),
    CONSTRAINT [FK_tab_inspecoes_anomalias_tab_anomalia_alertas] FOREIGN KEY ([ale_id]) REFERENCES [dbo].[tab_anomalia_alertas] ([ale_id]),
    CONSTRAINT [FK_tab_inspecoes_anomalias_tab_anomalia_causas] FOREIGN KEY ([aca_id]) REFERENCES [dbo].[tab_anomalia_causas] ([aca_id]),
    CONSTRAINT [FK_tab_inspecoes_anomalias_tab_anomalia_legendas] FOREIGN KEY ([leg_id]) REFERENCES [dbo].[tab_anomalia_legendas] ([leg_id]),
    CONSTRAINT [FK_tab_inspecoes_anomalias_tab_anomalia_tipos] FOREIGN KEY ([atp_id]) REFERENCES [dbo].[tab_anomalia_tipos] ([atp_id]),
    CONSTRAINT [FK_tab_inspecoes_anomalias_tab_inspecoes] FOREIGN KEY ([ins_id]) REFERENCES [dbo].[tab_inspecoes] ([ins_id]),
    CONSTRAINT [FK_tab_inspecoes_anomalias_tab_objetos] FOREIGN KEY ([obj_id]) REFERENCES [dbo].[tab_objetos] ([obj_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'Descrição', @value = N'Tabela de anomalias relacionadas a determmada inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador campo chave das anomalias de inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' estrangeira identificador de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'obj_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ins_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número da anomalia nessa inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_numero';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira tipo de anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'atp_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sigla da anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_sigla';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de nivel de alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ale_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quantidade de anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_quantidade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Espaçamento entre anomalia em cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_espacamento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Largura de anomalia em cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_largura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comprimento da anomalia em cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_comprimento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abertura mímima da anomalia em mm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_abertura_minima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abertura máxima da anomalia em mm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_abertura_maxima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de causa provável', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'aca_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número da fotografia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_fotografia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desenho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_desenho';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observações', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_observacoes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_anomalias', @level2type = N'COLUMN', @level2name = N'ian_atualizado_por';

