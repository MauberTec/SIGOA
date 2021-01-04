CREATE TABLE [dbo].[tab_objeto_conserva_politica] (
    [ocp_id]                         INT            NOT NULL,
    [ogv_id]                         INT            NOT NULL,
    [ogi_id_caracterizacao_situacao] INT            NULL,
    [ocp_descricao_alerta]           NVARCHAR (255) NULL,
    [ocp_descricao_servico]          NVARCHAR (255) NULL,
    [ocp_ativo]                      BIT            NOT NULL,
    [ocp_deletado]                   DATETIME       NULL,
    [ocp_data_criacao]               DATETIME       NOT NULL,
    [ocp_criado_por]                 INT            NOT NULL,
    [ocp_data_atualizacao]           DATETIME       NULL,
    [ocp_atualizado_por]             INT            NULL,
    CONSTRAINT [PK_tab_objeto_conserva_politica] PRIMARY KEY CLUSTERED ([ocp_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave primaria Identificador de politica de conserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva_politica', @level2type = N'COLUMN', @level2name = N'ocp_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira Identificador de variáveis de inspeção de um grupo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva_politica', @level2type = N'COLUMN', @level2name = N'ogv_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coluna de Carecterizacao da situacao - chave estrangeira ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva_politica', @level2type = N'COLUMN', @level2name = N'ogi_id_caracterizacao_situacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição do serviço de conserva a ser realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva_politica', @level2type = N'COLUMN', @level2name = N'ocp_descricao_servico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva_politica', @level2type = N'COLUMN', @level2name = N'ocp_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva_politica', @level2type = N'COLUMN', @level2name = N'ocp_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva_politica', @level2type = N'COLUMN', @level2name = N'ocp_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva_politica', @level2type = N'COLUMN', @level2name = N'ocp_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva_politica', @level2type = N'COLUMN', @level2name = N'ocp_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_conserva_politica', @level2type = N'COLUMN', @level2name = N'ocp_atualizado_por';

