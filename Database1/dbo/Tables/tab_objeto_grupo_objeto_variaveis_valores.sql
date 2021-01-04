CREATE TABLE [dbo].[tab_objeto_grupo_objeto_variaveis_valores] (
    [ovv_id]                         INT            NOT NULL,
    [ogv_id]                         INT            NOT NULL,
    [obj_id]                         BIGINT         NULL,
    [ogi_id_caracterizacao_situacao] INT            NULL,
    [ovv_observacoes_gerais]         NVARCHAR (255) NULL,
    [tpu_id]                         INT            NULL,
    [ovv_tpu_quantidade]             REAL           NULL,
    [ovv_ativo]                      BIT            NOT NULL,
    [ovv_deletado]                   DATETIME       NULL,
    [ovv_data_criacao]               DATETIME       NOT NULL,
    [ovv_criado_por]                 INT            NOT NULL,
    [ovv_data_atualizacao]           DATETIME       NULL,
    [ovv_atualizado_por]             INT            NULL,
    [ovv_descricao_servico]          NVARCHAR (255) NULL,
    [ovv_unidade_servico]            NVARCHAR (50)  NULL,
    CONSTRAINT [PK_tab_objeto_grupo_objeto_variaveis_valores] PRIMARY KEY CLUSTERED ([ovv_id] ASC),
    CONSTRAINT [FK_tab_objeto_grupo_objeto_variaveis_valores_tab_objeto_grupo_objeto_variaveis_valores] FOREIGN KEY ([ovv_id]) REFERENCES [dbo].[tab_objeto_grupo_objeto_variaveis_valores] ([ovv_id]),
    CONSTRAINT [FK_tab_objeto_grupo_objeto_variaveis_valores_tab_objeto_grupo_objetos_variaveis_itens] FOREIGN KEY ([ogi_id_caracterizacao_situacao]) REFERENCES [dbo].[tab_objeto_grupo_objetos_variaveis_itens] ([ogi_id]),
    CONSTRAINT [FK_tab_objeto_grupo_objeto_variaveis_valores_tab_objetos] FOREIGN KEY ([obj_id]) REFERENCES [dbo].[tab_objetos] ([obj_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave primaria Identificador do valor de variável de inspeção de um grupo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ovv_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira Identificador de variáveis de inspeção de um grupo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ogv_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de identificador de tipo de objeto, tipo grupo de objetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'obj_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coluna de Carecterizacao da situacao - chave estrangeira ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ogi_id_caracterizacao_situacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observações gerais', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ovv_observacoes_gerais';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'tpu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de TPUs', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ovv_tpu_quantidade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ovv_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ovv_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ovv_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ovv_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ovv_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ovv_atualizado_por';

