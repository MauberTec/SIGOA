CREATE TABLE [dbo].[tab_unidades_tipos] (
    [unt_id]               INT           NOT NULL,
    [unt_nome]             NVARCHAR (50) NOT NULL,
    [unt_ativo]            BIT           CONSTRAINT [DF_tab_unidades_tipos_unt_ativo] DEFAULT ((1)) NOT NULL,
    [unt_deletado]         DATETIME      NULL,
    [unt_data_criacao]     DATETIME      NOT NULL,
    [unt_criado_por]       INT           NOT NULL,
    [unt_data_atualizacao] DATETIME      NULL,
    [unt_atualizado_por]   INT           NULL,
    CONSTRAINT [PK_tab_tipos_unidades] PRIMARY KEY CLUSTERED ([unt_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tipo de unidade de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_tipos', @level2type = N'COLUMN', @level2name = N'unt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nome do tipo de unidade de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_tipos', @level2type = N'COLUMN', @level2name = N'unt_nome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_tipos', @level2type = N'COLUMN', @level2name = N'unt_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_tipos', @level2type = N'COLUMN', @level2name = N'unt_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_tipos', @level2type = N'COLUMN', @level2name = N'unt_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_tipos', @level2type = N'COLUMN', @level2name = N'unt_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_tipos', @level2type = N'COLUMN', @level2name = N'unt_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_unidades_tipos', @level2type = N'COLUMN', @level2name = N'unt_atualizado_por';

