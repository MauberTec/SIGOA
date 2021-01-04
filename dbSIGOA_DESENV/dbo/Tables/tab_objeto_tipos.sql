CREATE TABLE [dbo].[tab_objeto_tipos] (
    [tip_id]               INT            NOT NULL,
    [clo_id]               INT            NOT NULL,
    [tip_codigo]           NVARCHAR (15)  NULL,
    [tip_nome]             NVARCHAR (150) NOT NULL,
    [tip_descricao]        NVARCHAR (255) NOT NULL,
    [tip_pai]              INT            CONSTRAINT [DF_tab_objeto_tipos_tip_pai] DEFAULT ((-1)) NULL,
    [tip_ativo]            BIT            NOT NULL,
    [tip_deletado]         DATETIME       NULL,
    [tip_criado_por]       INT            NOT NULL,
    [tip_data_criacao]     DATETIME       NOT NULL,
    [tip_atualizado_por]   INT            NULL,
    [tip_data_atualizacao] DATETIME       NULL,
    CONSTRAINT [PK_tab_objeto_tipos_1] PRIMARY KEY CLUSTERED ([tip_id] ASC),
    CONSTRAINT [FK_tab_objeto_tipos_tab_objeto_classes] FOREIGN KEY ([clo_id]) REFERENCES [dbo].[tab_objeto_classes] ([clo_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tipo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'chave estrangeira identificador de classe de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'clo_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código do tipo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nome do tipo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_nome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de objeto pai', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_pai';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dat criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_tipos', @level2type = N'COLUMN', @level2name = N'tip_data_atualizacao';

