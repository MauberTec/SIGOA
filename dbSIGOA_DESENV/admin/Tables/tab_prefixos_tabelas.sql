CREATE TABLE [admin].[tab_prefixos_tabelas] (
    [pre_prefixo] NVARCHAR (3)   NOT NULL,
    [pre_tabela]  NVARCHAR (255) NULL,
    [pre_modulo]  NVARCHAR (255) NULL,
    CONSTRAINT [PK_pre_prefixo] PRIMARY KEY CLUSTERED ([pre_prefixo] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'Descrição', @value = N'Tablela com os prefixos utilizados nas tabelas do sistema SIGOA, os prefixos tem 3 caracteres, e são utilizados para identificar a que tabela o campo pertende originalmente', @level0type = N'SCHEMA', @level0name = N'admin', @level1type = N'TABLE', @level1name = N'tab_prefixos_tabelas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfixo usado na tabela- 3 primeiros caracteres nos campos ', @level0type = N'SCHEMA', @level0name = N'admin', @level1type = N'TABLE', @level1name = N'tab_prefixos_tabelas', @level2type = N'COLUMN', @level2name = N'pre_prefixo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabela que usa o prefixo', @level0type = N'SCHEMA', @level0name = N'admin', @level1type = N'TABLE', @level1name = N'tab_prefixos_tabelas', @level2type = N'COLUMN', @level2name = N'pre_tabela';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Módulo do sistem SIGOA da Tabela', @level0type = N'SCHEMA', @level0name = N'admin', @level1type = N'TABLE', @level1name = N'tab_prefixos_tabelas', @level2type = N'COLUMN', @level2name = N'pre_modulo';

