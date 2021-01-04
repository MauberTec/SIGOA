CREATE TABLE [dbo].[tab_objeto_conserva] (
    [oco_id]                  INT      NOT NULL,
    [ovv_id]                  INT      NOT NULL,
    [oco_quantidade_conserva] INT      NOT NULL,
    [ovv_ativo]               BIT      NOT NULL,
    [ovv_deletado]            DATETIME NULL,
    [ovv_data_criacao]        DATETIME NOT NULL,
    [ovv_criado_por]          INT      NOT NULL,
    [ovv_data_atualizacao]    DATETIME NULL,
    [ovv_atualizado_por]      INT      NULL,
    CONSTRAINT [PK_tab_objeto_conserva] PRIMARY KEY CLUSTERED ([oco_id] ASC),
    CONSTRAINT [FK_tab_objeto_conserva_tab_objeto_grupo_objeto_variaveis_valores] FOREIGN KEY ([ovv_id]) REFERENCES [dbo].[tab_objeto_grupo_objeto_variaveis_valores] ([ovv_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave primaria identificador de conserva de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva', @level2type = N'COLUMN', @level2name = N'oco_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira Identificador de variáveis de inspeção de um grupo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva', @level2type = N'COLUMN', @level2name = N'ovv_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quantidate conserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva', @level2type = N'COLUMN', @level2name = N'oco_quantidade_conserva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva', @level2type = N'COLUMN', @level2name = N'ovv_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva', @level2type = N'COLUMN', @level2name = N'ovv_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva', @level2type = N'COLUMN', @level2name = N'ovv_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva', @level2type = N'COLUMN', @level2name = N'ovv_criado_por';

