CREATE TABLE [dbo].[tab_ordem_servico_classes] (
    [ocl_id]               INT            NOT NULL,
    [ocl_codigo]           NVARCHAR (50)  NOT NULL,
    [ocl_descricao]        NVARCHAR (255) NOT NULL,
    [ocl_ativo]            BIT            NOT NULL,
    [ocl_deletado]         DATETIME       NULL,
    [ocl_criado_por]       INT            NOT NULL,
    [ocl_data_criacao]     DATETIME       NOT NULL,
    [ocl_atualizado_por]   INT            NULL,
    [ocl_data_atualizacao] DATETIME       NULL,
    CONSTRAINT [PK_tab_os_classes] PRIMARY KEY CLUSTERED ([ocl_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de classe de OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_classes', @level2type = N'COLUMN', @level2name = N'ocl_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código da OS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_classes', @level2type = N'COLUMN', @level2name = N'ocl_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_classes', @level2type = N'COLUMN', @level2name = N'ocl_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_classes', @level2type = N'COLUMN', @level2name = N'ocl_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_classes', @level2type = N'COLUMN', @level2name = N'ocl_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_classes', @level2type = N'COLUMN', @level2name = N'ocl_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_classes', @level2type = N'COLUMN', @level2name = N'ocl_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_classes', @level2type = N'COLUMN', @level2name = N'ocl_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_ordem_servico_classes', @level2type = N'COLUMN', @level2name = N'ocl_data_atualizacao';

