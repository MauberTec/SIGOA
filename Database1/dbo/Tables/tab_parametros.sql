CREATE TABLE [dbo].[tab_parametros] (
    [par_id]               NVARCHAR (200)  NOT NULL,
    [par_valor]            NVARCHAR (3000) NULL,
    [par_descricao]        NVARCHAR (255)  NOT NULL,
    [par_data_criacao]     DATETIME        CONSTRAINT [DF__PARAMETRO__CREAT__628FA481] DEFAULT (getdate()) NULL,
    [par_criado_por]       INT             NULL,
    [par_data_atualizacao] DATETIME        CONSTRAINT [DF__PARAMETRO__UPDAT__6383C8BA] DEFAULT (getdate()) NULL,
    [par_atualizado_por]   INT             NULL,
    CONSTRAINT [PK_tab_parametros] PRIMARY KEY CLUSTERED ([par_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de parâmetro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_parametros', @level2type = N'COLUMN', @level2name = N'par_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor do Parâmetro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_parametros', @level2type = N'COLUMN', @level2name = N'par_valor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_parametros', @level2type = N'COLUMN', @level2name = N'par_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_parametros', @level2type = N'COLUMN', @level2name = N'par_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_parametros', @level2type = N'COLUMN', @level2name = N'par_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dat atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_parametros', @level2type = N'COLUMN', @level2name = N'par_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_parametros', @level2type = N'COLUMN', @level2name = N'par_atualizado_por';

