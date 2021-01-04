CREATE TABLE [dbo].[tab_objeto_grupo_objeto_variaveis_old] (
    [ogv_id]               INT            NOT NULL,
    [tip_id]               INT            NOT NULL,
    [ogv_nome]             NVARCHAR (80)  NULL,
    [ogv_descricao]        NVARCHAR (255) NULL,
    [uni_id]               INT            NULL,
    [ogv_ativo]            BIT            NOT NULL,
    [ogv_deletado]         DATETIME       NULL,
    [ogv_data_criacao]     DATETIME       NOT NULL,
    [ogv_criado_por]       BIGINT         NOT NULL,
    [ogv_data_atualizacao] DATETIME       NULL,
    [ogv_atualizado_por]   BIGINT         NULL,
    CONSTRAINT [PK_tab_objeto_grupoobj_variaveis_old] PRIMARY KEY CLUSTERED ([ogv_id] ASC),
    CONSTRAINT [FK_tab_objeto_grupo_objeto_variaveis_tab_objeto_tipos1] FOREIGN KEY ([tip_id]) REFERENCES [dbo].[tab_objeto_tipos] ([tip_id]),
    CONSTRAINT [FK_tab_objeto_grupo_objeto_variaveis_tab_unidades_medida1] FOREIGN KEY ([uni_id]) REFERENCES [dbo].[tab_unidades_medida] ([uni_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave primaria Identificador de variáveis de inspeção de um grupo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'ogv_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de identificador de tipo de objeto, tipo grupo de objetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'tip_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nome da variável', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'ogv_nome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'ogv_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de unidade', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'uni_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'ogv_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'ogv_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'ogv_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'ogv_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'ogv_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_old', @level2type = N'COLUMN', @level2name = N'ogv_atualizado_por';

