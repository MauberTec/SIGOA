CREATE TABLE [dbo].[tab_objeto_classes] (
    [clo_id]               INT            NOT NULL,
    [clo_nome]             NVARCHAR (50)  NOT NULL,
    [clo_descricao]        NVARCHAR (255) NOT NULL,
    [clo_ativo]            BIT            NOT NULL,
    [clo_deletado]         DATETIME       NULL,
    [clo_criado_por]       INT            NOT NULL,
    [clo_data_criacao]     DATETIME       NOT NULL,
    [clo_atualizado_por]   INT            NULL,
    [clo_data_atualizacao] DATETIME       NULL,
    CONSTRAINT [PK_tab_objeto_classes] PRIMARY KEY CLUSTERED ([clo_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de classe de objetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_classes', @level2type = N'COLUMN', @level2name = N'clo_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nome da classe de objetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_classes', @level2type = N'COLUMN', @level2name = N'clo_nome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição da classe de objetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_classes', @level2type = N'COLUMN', @level2name = N'clo_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_classes', @level2type = N'COLUMN', @level2name = N'clo_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_classes', @level2type = N'COLUMN', @level2name = N'clo_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_classes', @level2type = N'COLUMN', @level2name = N'clo_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_classes', @level2type = N'COLUMN', @level2name = N'clo_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_classes', @level2type = N'COLUMN', @level2name = N'clo_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_classes', @level2type = N'COLUMN', @level2name = N'clo_data_atualizacao';

