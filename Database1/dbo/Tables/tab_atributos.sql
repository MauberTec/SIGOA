CREATE TABLE [dbo].[tab_atributos] (
    [atr_id]                 INT            NOT NULL,
    [tip_id]                 INT            NOT NULL,
    [clo_id]                 INT            NOT NULL,
    [atr_atributo_nome]      NVARCHAR (150) NOT NULL,
    [atr_descricao]          NVARCHAR (255) NOT NULL,
    [atr_mascara_texto]      NVARCHAR (50)  NULL,
    [unt_id]                 INT            NULL,
    [atr_herdavel]           BIT            CONSTRAINT [DF_tab_objeto_atributos_fixos_atf_herdavel] DEFAULT ((1)) NOT NULL,
    [atr_ativo]              BIT            NOT NULL,
    [atr_apresentacao_itens] NVARCHAR (20)  NULL,
    [atr_deletado]           DATE           NULL,
    [atr_criado_por]         INT            NOT NULL,
    [atr_data_criacao]       DATETIME       NOT NULL,
    [atr_atualizado_por]     INT            NULL,
    [atr_data_atualizacao]   DATETIME       NULL,
    [atr_atributo_funcional] BIT            NULL,
    CONSTRAINT [PK_objeto_atributos_fixos] PRIMARY KEY CLUSTERED ([atr_id] ASC),
    CONSTRAINT [FK_tab_atributos_tab_unidades_tipos] FOREIGN KEY ([unt_id]) REFERENCES [dbo].[tab_unidades_tipos] ([unt_id]),
    CONSTRAINT [FK_tab_objeto_atributos_fixos_tab_objeto_classes] FOREIGN KEY ([clo_id]) REFERENCES [dbo].[tab_objeto_classes] ([clo_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Atributos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de tipo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'tip_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira classe de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'clo_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nome do atributo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_atributo_nome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mascra para colocar na tela', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_mascara_texto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'unidade do atributo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'unt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se o atributo é herdável', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_herdavel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apresentação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_apresentacao_itens';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se é atributoto funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributos', @level2type = N'COLUMN', @level2name = N'atr_atributo_funcional';

