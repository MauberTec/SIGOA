CREATE TABLE [dbo].[tab_ordens_servico] (
    [ord_id]                       BIGINT         NOT NULL,
    [ord_codigo]                   NVARCHAR (200) NOT NULL,
    [ord_descricao]                NVARCHAR (255) NOT NULL,
    [ord_sequencial_tipo]          BIGINT         NULL,
    [ord_pai]                      BIGINT         NULL,
    [ocl_id]                       INT            NULL,
    [tos_id]                       INT            NULL,
    [sos_id]                       INT            NULL,
    [obj_id]                       BIGINT         NULL,
    [ord_ativo]                    BIT            CONSTRAINT [DF_tab_ordem_servico_ord_ativo] DEFAULT ((1)) NOT NULL,
    [ord_deletado]                 DATETIME       NULL,
    [ord_data_criacao]             DATETIME       CONSTRAINT [DF_tab_ordem_servico_oss_data_criacao] DEFAULT (getdate()) NOT NULL,
    [ord_criado_por]               INT            CONSTRAINT [DF_tab_ordem_servico_oss_criado_por] DEFAULT ((1)) NOT NULL,
    [ord_data_atualizacao]         DATETIME       NULL,
    [ord_atualizado_por]           INT            NULL,
    [ord_criticidade]              INT            NULL,
    [con_id]                       INT            NULL,
    [ord_data_inicio_programada]   DATE           NULL,
    [ord_data_termino_programada]  DATE           NULL,
    [ord_data_inicio_execucao]     DATE           NULL,
    [ord_data_termino_execucao]    DATE           NULL,
    [ord_quantidade_estimada]      FLOAT (53)     NULL,
    [uni_id_qt_estimada]           INT            NULL,
    [ord_quantidade_executada]     FLOAT (53)     NULL,
    [uni_id_qt_executada]          INT            NULL,
    [ord_custo_estimado]           FLOAT (53)     NULL,
    [ord_custo_final]              FLOAT (53)     NULL,
    [ord_aberta_por]               INT            NULL,
    [ord_data_abertura]            DATETIME       NULL,
    [ord_responsavel_der]          NCHAR (100)    NULL,
    [ord_responsavel_fiscalizacao] NCHAR (100)    NULL,
    [con_id_fiscalizacao]          INT            NULL,
    [ord_responsavel_execucao]     NCHAR (100)    NULL,
    [con_id_execucao]              INT            NULL,
    [ord_responsavel_suspensao]    NCHAR (100)    NULL,
    [ord_data_suspensao]           DATE           NULL,
    [ord_responsavel_cancelamento] NCHAR (100)    NULL,
    [ord_data_cancelamento]        DATE           NULL,
    [ord_data_reinicio]            DATE           NULL,
    [con_id_orcamento]             BIGINT         NULL,
    [tpt_id]                       CHAR (1)       NULL,
    [tpu_data_base_der]            DATE           NULL,
    [tpu_id]                       INT            NULL,
    [tpu_preco_unitario]           REAL           NULL,
    CONSTRAINT [PK_tab_ordem_servicos] PRIMARY KEY CLUSTERED ([ord_id] ASC),
    CONSTRAINT [FK_tab_ordens_servico_tab_contratos] FOREIGN KEY ([con_id]) REFERENCES [dbo].[tab_contratos] ([con_id]),
    CONSTRAINT [FK_tab_ordens_servico_tab_objetos] FOREIGN KEY ([obj_id]) REFERENCES [dbo].[tab_objetos] ([obj_id]),
    CONSTRAINT [FK_tab_ordens_servico_tab_ordem_servico_classes] FOREIGN KEY ([ocl_id]) REFERENCES [dbo].[tab_ordem_servico_classes] ([ocl_id]),
    CONSTRAINT [FK_tab_ordens_servico_tab_ordem_servico_status] FOREIGN KEY ([sos_id]) REFERENCES [dbo].[tab_ordem_servico_status] ([sos_id]),
    CONSTRAINT [FK_tab_ordens_servico_tab_ordem_servico_tipos] FOREIGN KEY ([tos_id]) REFERENCES [dbo].[tab_ordem_servico_tipos] ([tos_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código da OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sequencial de tipo de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_sequencial_tipo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'OS pai', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_pai';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave estrangeira classe de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ocl_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de tipo de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'tos_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de status de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'sos_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'obj_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'criticidade', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_criticidade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'chave estrangeira identificador de contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'con_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data programada para incio da OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_data_inicio_programada';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data programada para término da OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_data_termino_programada';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data de início de execução da OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_data_inicio_execucao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data término da execução da OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_data_termino_execucao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quantidade estimada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_quantidade_estimada';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidade da quantidade estimada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'uni_id_qt_estimada';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Qauntidade executada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_quantidade_executada';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'unidade da quantidade executada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'uni_id_qt_executada';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Custo estimado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_custo_estimado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Custo final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_custo_final';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem briu a OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_aberta_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dat Abertura da OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_data_abertura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reponsável DER da OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_responsavel_der';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Responsável Fiscalização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_responsavel_fiscalizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador do contrato de ficaliação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'con_id_fiscalizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Responsável execução', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_responsavel_execucao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador do contrato de execução', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'con_id_execucao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Responsável pela suspensão da OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_responsavel_suspensao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data suspensão da OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_data_suspensao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Responsável cancelamento OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_responsavel_cancelamento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Daya cancelamento OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_data_cancelamento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Reinício OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'ord_data_reinicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ontrato de orçamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'con_id_orcamento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de preço de tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'tpt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data da base da TPU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'tpu_data_base_der';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de TPU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'tpu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TPU preço unitário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordens_servico', @level2type = N'COLUMN', @level2name = N'tpu_preco_unitario';

