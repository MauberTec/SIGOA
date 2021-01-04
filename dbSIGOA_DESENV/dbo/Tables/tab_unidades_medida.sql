CREATE TABLE [dbo].[tab_unidades_medida] (
    [uni_id]               INT            NOT NULL,
    [unt_id]               INT            NOT NULL,
    [uni_unidade]          NVARCHAR (10)  NOT NULL,
    [uni_descricao]        NVARCHAR (255) NOT NULL,
    [uni_pai]              INT            NULL,
    [uni_fator_conversao]  REAL           NULL,
    [uni_ativo]            BIT            CONSTRAINT [DF_tab_unidades_medida_uni_ativo] DEFAULT ((1)) NOT NULL,
    [uni_deletado]         DATETIME       NULL,
    [uni_data_criacao]     DATETIME       NOT NULL,
    [uni_criado_por]       INT            NULL,
    [uni_data_atualizacao] DATETIME       NULL,
    [uni_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_unidades_medida] PRIMARY KEY CLUSTERED ([uni_id] ASC),
    CONSTRAINT [FK_tab_unidades_medida_tab_unidades_tipos] FOREIGN KEY ([unt_id]) REFERENCES [dbo].[tab_unidades_tipos] ([unt_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidade de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira tipo de unidade de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'unt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidade de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_unidade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidade pai', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_pai';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fator de conversão', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_fator_conversao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_medida', @level2type = N'COLUMN', @level2name = N'uni_atualizado_por';

