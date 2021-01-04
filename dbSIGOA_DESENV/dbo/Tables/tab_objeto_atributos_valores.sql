CREATE TABLE [dbo].[tab_objeto_atributos_valores] (
    [atv_id]               BIGINT         NOT NULL,
    [obj_id]               BIGINT         NOT NULL,
    [atr_id]               INT            NOT NULL,
    [ati_id]               INT            NULL,
    [atv_valor]            NVARCHAR (MAX) NULL,
    [uni_id]               BIGINT         NULL,
    [atv_ativo]            BIT            NOT NULL,
    [atv_deletado]         DATETIME       NULL,
    [atv_data_criacao]     DATETIME       NOT NULL,
    [atv_criado_por]       BIGINT         NOT NULL,
    [atv_data_atualizacao] DATETIME       NULL,
    [atv_atualizado_por]   BIGINT         NULL,
    CONSTRAINT [PK_tab_objeto_atributos_fixos_valores] PRIMARY KEY CLUSTERED ([atv_id] ASC),
    CONSTRAINT [FK_tab_objeto_atributos_fixos_valores_tab_obj_atributos_fixos] FOREIGN KEY ([atr_id]) REFERENCES [dbo].[tab_atributos] ([atr_id]),
    CONSTRAINT [FK_tab_objeto_atributos_fixos_valores_tab_objeto_atributo_fixo_itens] FOREIGN KEY ([ati_id]) REFERENCES [dbo].[tab_atributo_itens] ([ati_id]),
    CONSTRAINT [FK_tab_objeto_atributos_fixos_valores_tab_objetos] FOREIGN KEY ([obj_id]) REFERENCES [dbo].[tab_objetos] ([obj_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador atributo valor de objeto ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'atv_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de identificador de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'obj_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeiraidentificador de atributo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'atr_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de atributo item', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'ati_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor do Atributo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'atv_valor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de unidade', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'uni_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'atv_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'atv_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'atv_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'atv_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'atv_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_atributos_valores', @level2type = N'COLUMN', @level2name = N'atv_atualizado_por';

