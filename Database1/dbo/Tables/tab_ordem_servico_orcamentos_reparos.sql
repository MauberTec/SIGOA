CREATE TABLE [dbo].[tab_ordem_servico_orcamentos_reparos] (
    [ore_id]               INT      NOT NULL,
    [orc_id]               INT      NOT NULL,
    [obj_id_oae]           BIGINT   NOT NULL,
    [obj_id_conserva]      BIGINT   NULL,
    [ovv_id]               INT      NULL,
    [ire_id]               INT      NULL,
    [ore_ativo]            BIT      NOT NULL,
    [ore_deletado]         DATETIME NULL,
    [ore_data_criacao]     DATETIME NOT NULL,
    [ore_criado_por]       INT      NULL,
    [ore_data_atualizacao] DATETIME NULL,
    [ore_atualizado_por]   INT      NULL,
    CONSTRAINT [PK_tab_ordem_servico_orcamentos_reparos] PRIMARY KEY CLUSTERED ([ore_id] ASC),
    CONSTRAINT [FK_tab_ordem_servico_orcamentos_reparos_tab_inspecoes_reparos] FOREIGN KEY ([ire_id]) REFERENCES [dbo].[tab_inspecoes_reparos] ([ire_id]),
    CONSTRAINT [FK_tab_ordem_servico_orcamentos_reparos_tab_objeto_grupo_objeto_variaveis_valores] FOREIGN KEY ([ovv_id]) REFERENCES [dbo].[tab_objeto_grupo_objeto_variaveis_valores] ([ovv_id]),
    CONSTRAINT [FK_tab_ordem_servico_orcamentos_reparos_tab_objetos] FOREIGN KEY ([obj_id_oae]) REFERENCES [dbo].[tab_objetos] ([obj_id]),
    CONSTRAINT [FK_tab_ordem_servico_orcamentos_reparos_tab_objetos1] FOREIGN KEY ([obj_id_conserva]) REFERENCES [dbo].[tab_objetos] ([obj_id]),
    CONSTRAINT [FK_tab_ordem_servico_orcamentos_reparos_tab_ordem_servico_orcamentos] FOREIGN KEY ([orc_id]) REFERENCES [dbo].[tab_ordem_servico_orcamentos] ([orc_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de reparo ou conserva de um orçamento ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'ore_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de orçamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'orc_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de de identificador de OAE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'obj_id_oae';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de de identificador de serviço de grupo de objeto - conserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'obj_id_conserva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de de identificador de variavel de grupo de objetos - serviço de conserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'ovv_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de de identificador de reparo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'ire_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'ore_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'ore_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'ore_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'ore_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'ore_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_orcamentos_reparos', @level2type = N'COLUMN', @level2name = N'ore_atualizado_por';

