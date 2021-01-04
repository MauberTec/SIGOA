CREATE TABLE [dbo].[tab_objeto_grupo_objetos_variaveis_itens] (
    [ogi_id]               INT            NOT NULL,
    [ogv_id]               INT            NOT NULL,
    [ogi_item]             NVARCHAR (100) NOT NULL,
    [ogi_ativo]            BIT            NOT NULL,
    [ogi_deletado]         DATETIME       NULL,
    [ogi_data_criacao]     DATETIME       NOT NULL,
    [ogi_criado_por]       INT            NOT NULL,
    [ogi_data_atualizacao] DATETIME       NULL,
    [ogi_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_objeto_grupo_objeto_itens] PRIMARY KEY CLUSTERED ([ogi_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave primária', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_grupo_objetos_variaveis_itens', @level2type = N'COLUMN', @level2name = N'ogi_id';

