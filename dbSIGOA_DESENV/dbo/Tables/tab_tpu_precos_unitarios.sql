CREATE TABLE [dbo].[tab_tpu_precos_unitarios] (
    [tpu_id]               INT            NOT NULL,
    [fas_id]               INT            NOT NULL,
    [tpt_id]               CHAR (1)       NOT NULL,
    [tpu_data_base_der]    DATE           NOT NULL,
    [tpu_codigo_der]       NVARCHAR (100) NOT NULL,
    [tpu_descricao]        NVARCHAR (255) NOT NULL,
    [uni_id]               INT            NOT NULL,
    [moe_id]               CHAR (3)       NOT NULL,
    [tpu_preco_unitario]   REAL           NOT NULL,
    [tpu_tipo_unidade]     NVARCHAR (50)  NULL,
    [tpu_preco_calculado]  NVARCHAR (50)  NULL,
    [tpu_ativo]            BIT            CONSTRAINT [DF_tab_tpu_precos_unitarios_tpu_ativo] DEFAULT ((1)) NOT NULL,
    [tpu_deletado]         DATETIME       NULL,
    [tpu_data_criacao]     DATETIME       CONSTRAINT [DF_tab_tpu_precos_unitarios_tpu_data_criacao] DEFAULT (getdate()) NOT NULL,
    [tpu_criado_por]       INT            CONSTRAINT [DF_tab_tpu_precos_unitarios_tpu_criado_por] DEFAULT ((0)) NOT NULL,
    [tpu_data_atualizacao] DATETIME       NULL,
    [tpu_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_precos_unitarios_1] PRIMARY KEY CLUSTERED ([tpu_id] ASC),
    CONSTRAINT [FK_tab_tpu_precos_unitarios_tab_moedas] FOREIGN KEY ([moe_id]) REFERENCES [dbo].[tab_moedas] ([moe_id]),
    CONSTRAINT [FK_tab_tpu_precos_unitarios_tab_tpu_fase] FOREIGN KEY ([fas_id]) REFERENCES [dbo].[tab_tpu_fases] ([fas_id]),
    CONSTRAINT [FK_tab_tpu_precos_unitarios_tab_tpu_tipo_preco] FOREIGN KEY ([tpt_id]) REFERENCES [dbo].[tab_tpu_tipos_preco] ([tpt_id]),
    CONSTRAINT [FK_tab_tpu_precos_unitarios_tab_unidades_medida] FOREIGN KEY ([uni_id]) REFERENCES [dbo].[tab_unidades_medida] ([uni_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de TPU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira fase tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'fas_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira tipo tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpt_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data base tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_data_base_der';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código DER tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_codigo_der';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dercrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira unidade tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'uni_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira moeda tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'moe_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preço unitário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_preco_unitario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo unidade tpu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_tipo_unidade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preço carculado CPU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_preco_calculado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_tpu_precos_unitarios', @level2type = N'COLUMN', @level2name = N'tpu_atualizado_por';

