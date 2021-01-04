CREATE TABLE [dbo].[tab_inspecao_atributos_valores] (
    [iav_id]               BIGINT         NOT NULL,
    [ins_id]               BIGINT         NOT NULL,
    [atr_id]               INT            NOT NULL,
    [ati_id]               INT            NULL,
    [iav_valor]            NVARCHAR (MAX) NOT NULL,
    [uni_id]               INT            NULL,
    [iav_ativo]            BIT            NOT NULL,
    [iav_deletado]         DATETIME       NULL,
    [iav_data_criacao]     DATETIME       NOT NULL,
    [iav_criado_por]       INT            NOT NULL,
    [iav_data_atualizacao] DATETIME       NULL,
    [iav_atualizado_por]   INT            NULL,
    [obj_id]               BIGINT         NOT NULL,
    CONSTRAINT [PK_tab_inspecao_atributos_valores] PRIMARY KEY CLUSTERED ([iav_id] ASC),
    CONSTRAINT [FK_tab_inspecao_atributos_valores_tab_atributo_itens] FOREIGN KEY ([ati_id]) REFERENCES [dbo].[tab_atributo_itens] ([ati_id]),
    CONSTRAINT [FK_tab_inspecao_atributos_valores_tab_atributos] FOREIGN KEY ([atr_id]) REFERENCES [dbo].[tab_atributos] ([atr_id]),
    CONSTRAINT [FK_tab_inspecao_atributos_valores_tab_inspecoes] FOREIGN KEY ([ins_id]) REFERENCES [dbo].[tab_inspecoes] ([ins_id]),
    CONSTRAINT [FK_tab_inspecao_atributos_valores_tab_objetos] FOREIGN KEY ([obj_id]) REFERENCES [dbo].[tab_objetos] ([obj_id]),
    CONSTRAINT [FK_tab_inspecao_atributos_valores_tab_unidades_medida] FOREIGN KEY ([uni_id]) REFERENCES [dbo].[tab_unidades_medida] ([uni_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de valor de atributo de inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'iav_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'ins_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de atributo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'atr_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de item de atributo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'ati_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor do atributo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'iav_valor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'unidade', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'uni_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'iav_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'iav_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'iav_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'iav_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'iav_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'iav_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Objeto da inspeção', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecao_atributos_valores', @level2type = N'COLUMN', @level2name = N'obj_id';

