CREATE TABLE [dbo].[tab_atributo_itens] (
    [ati_id]               INT             NOT NULL,
    [atr_id]               INT             NOT NULL,
    [ati_item]             NVARCHAR (1000) NOT NULL,
    [ati_ativo]            BIT             CONSTRAINT [DF_tab_objeto_atributo_fixo_itens_ati_ativo] DEFAULT ((1)) NOT NULL,
    [ati_deletado]         DATETIME        NULL,
    [ati_data_criacao]     DATETIME        CONSTRAINT [DF_tab_objeto_atributo_fixo_itens_ati_data_criacao] DEFAULT (getdate()) NOT NULL,
    [ati_criado_por]       BIGINT          CONSTRAINT [DF_tab_objeto_atributo_fixo_itens_ati_criado_por] DEFAULT ((0)) NOT NULL,
    [ati_data_atualizacao] DATETIME        NULL,
    [ati_atualizado_por]   BIGINT          NULL,
    CONSTRAINT [PK_tab_objeto_atributo_fixo_itens_1] PRIMARY KEY CLUSTERED ([ati_id] ASC),
    CONSTRAINT [FK_tab_objeto_atributo_fixo_itens_tab_objeto_atributos_fixos] FOREIGN KEY ([atr_id]) REFERENCES [dbo].[tab_atributos] ([atr_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'Descrição', @value = N'Tabela  com os atributos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributo_itens';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indentificador de atributo de itens', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributo_itens', @level2type = N'COLUMN', @level2name = N'ati_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de identificador de atributo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributo_itens', @level2type = N'COLUMN', @level2name = N'atr_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Item de atributo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributo_itens', @level2type = N'COLUMN', @level2name = N'ati_item';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributo_itens', @level2type = N'COLUMN', @level2name = N'ati_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributo_itens', @level2type = N'COLUMN', @level2name = N'ati_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributo_itens', @level2type = N'COLUMN', @level2name = N'ati_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributo_itens', @level2type = N'COLUMN', @level2name = N'ati_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributo_itens', @level2type = N'COLUMN', @level2name = N'ati_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_atributo_itens', @level2type = N'COLUMN', @level2name = N'ati_atualizado_por';

