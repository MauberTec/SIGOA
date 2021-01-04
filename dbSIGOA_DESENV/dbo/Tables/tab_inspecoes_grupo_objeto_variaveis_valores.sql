CREATE TABLE [dbo].[tab_inspecoes_grupo_objeto_variaveis_valores] (
    [ivv_id]                         INT            NOT NULL,
    [ins_id]                         BIGINT         NULL,
    [ogv_id]                         INT            NOT NULL,
    [obj_id]                         BIGINT         NULL,
    [ogi_id_caracterizacao_situacao] INT            NULL,
    [ivv_observacoes_gerais]         NVARCHAR (255) NULL,
    [tpu_id]                         INT            NULL,
    [ivv_tpu_quantidade]             REAL           NULL,
    [ivv_ativo]                      BIT            NOT NULL,
    [ivv_deletado]                   DATETIME       NULL,
    [ivv_data_criacao]               DATETIME       NOT NULL,
    [ivv_criado_por]                 INT            NOT NULL,
    [ivv_data_atualizacao]           DATETIME       NULL,
    [ivv_atualizado_por]             INT            NULL,
    [ivv_descricao_servico]          NVARCHAR (255) NULL,
    [ivv_unidade_servico]            NVARCHAR (50)  NULL,
    CONSTRAINT [PK_tab_inspecoes_grupo_objeto_variaveis_valores] PRIMARY KEY CLUSTERED ([ivv_id] ASC),
    CONSTRAINT [FK_tab_inspecoes_grupo_objeto_variaveis_valores_tab_inspecoes_grupo_objeto_variaveis_valores] FOREIGN KEY ([ivv_id]) REFERENCES [dbo].[tab_inspecoes_grupo_objeto_variaveis_valores] ([ivv_id]),
    CONSTRAINT [FK_tab_inspecoes_grupo_objeto_variaveis_valores_tab_objeto_grupo_objetos_variaveis_itens] FOREIGN KEY ([ogi_id_caracterizacao_situacao]) REFERENCES [dbo].[tab_objeto_grupo_objetos_variaveis_itens] ([ogi_id]),
    CONSTRAINT [FK_tab_inspecoes_grupo_objeto_variaveis_valores_tab_objetos] FOREIGN KEY ([obj_id]) REFERENCES [dbo].[tab_objetos] ([obj_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave primaria Identificador do valor de variável de inspeção de um grupo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ivv_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira Identificador de variáveis de inspeção de um grupo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ogv_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de identificador de tipo de objeto, tipo grupo de objetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'obj_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coluna de Carecterizacao da situacao - chave estrangeira ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ogi_id_caracterizacao_situacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observações gerais', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ivv_observacoes_gerais';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'tpu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de TPUs', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ivv_tpu_quantidade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ivv_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ivv_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ivv_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ivv_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ivv_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_inspecoes_grupo_objeto_variaveis_valores', @level2type = N'COLUMN', @level2name = N'ivv_atualizado_por';

