CREATE TABLE [dbo].[tab_inspecoes] (
    [ins_id]               BIGINT         NOT NULL,
    [ipt_id]               INT            NOT NULL,
    [obj_id]               BIGINT         NOT NULL,
    [ord_id]               BIGINT         NOT NULL,
    [ins_ativo]            BIT            NOT NULL,
    [ins_deletado]         DATETIME       NULL,
    [ins_data_criacao]     DATETIME       NOT NULL,
    [ins_criado_por]       INT            NOT NULL,
    [ins_data_atualizacao] DATETIME       NULL,
    [ins_atualizado_por]   INT            NULL,
    [ins_documento]        NVARCHAR (255) NULL,
    [ins_data]             NVARCHAR (15)  NULL,
    [ins_executantes]      NVARCHAR (500) NULL,
    [ins_pontuacaoOAE]     NVARCHAR (5)   NULL,
    [ins_documento_2]      NVARCHAR (255) NULL,
    [ins_data_2]           NVARCHAR (15)  NULL,
    [ins_executantes_2]    NVARCHAR (500) NULL,
    [ins_pontuacaoOAE_2]   NVARCHAR (5)   NULL,
    [ins_documento_3]      NVARCHAR (255) NULL,
    [ins_data_3]           NVARCHAR (15)  NULL,
    [ins_executantes_3]    NVARCHAR (500) NULL,
    [ins_pontuacaoOAE_3]   NVARCHAR (5)   NULL,
    [ins_anom_Responsavel] NVARCHAR (255) NULL,
    [ins_anom_data]        NVARCHAR (15)  NULL,
    [ins_anom_quadroA_1]   NVARCHAR (3)   NULL,
    [ins_anom_quadroA_2]   NVARCHAR (500) NULL,
    [ins_tos_id_1]         INT            NULL,
    [ins_tos_id_2]         INT            NULL,
    [ins_tos_id_3]         INT            NULL,
    CONSTRAINT [PK_tab_inspecoes] PRIMARY KEY CLUSTERED ([ins_id] ASC),
    CONSTRAINT [FK_tab_inspecoes_tab_inspecao_tipos] FOREIGN KEY ([ipt_id]) REFERENCES [dbo].[tab_inspecao_tipos] ([ipt_id]),
    CONSTRAINT [FK_tab_inspecoes_tab_objetos] FOREIGN KEY ([obj_id]) REFERENCES [dbo].[tab_objetos] ([obj_id]),
    CONSTRAINT [FK_tab_inspecoes_tab_ordens_servico] FOREIGN KEY ([ord_id]) REFERENCES [dbo].[tab_ordens_servico] ([ord_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes', @level2type = N'COLUMN', @level2name = N'ins_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de tipo de inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes', @level2type = N'COLUMN', @level2name = N'ipt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cgave estrangeira identificador de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes', @level2type = N'COLUMN', @level2name = N'obj_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes', @level2type = N'COLUMN', @level2name = N'ord_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes', @level2type = N'COLUMN', @level2name = N'ins_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes', @level2type = N'COLUMN', @level2name = N'ins_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes', @level2type = N'COLUMN', @level2name = N'ins_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes', @level2type = N'COLUMN', @level2name = N'ins_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes', @level2type = N'COLUMN', @level2name = N'ins_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes', @level2type = N'COLUMN', @level2name = N'ins_atualizado_por';

