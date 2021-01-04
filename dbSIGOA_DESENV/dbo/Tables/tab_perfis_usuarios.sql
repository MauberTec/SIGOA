CREATE TABLE [dbo].[tab_perfis_usuarios] (
    [pfu_id]               INT      IDENTITY (1, 1) NOT NULL,
    [per_id]               INT      NOT NULL,
    [usu_id]               INT      NOT NULL,
    [pfu_deletado]         DATETIME NULL,
    [pfu_data_criacao]     DATETIME CONSTRAINT [DF__PERFILUSU__CREAT__05D8E0BE] DEFAULT (getdate()) NULL,
    [pfu_criado_por]       INT      NULL,
    [pfu_data_atualizacao] DATETIME CONSTRAINT [DF__PERFILUSU__UPDAT__06CD04F7] DEFAULT (getdate()) NULL,
    [pfu_atualizado_por]   INT      NULL,
    CONSTRAINT [PK_pfu_perfil_id] PRIMARY KEY CLUSTERED ([pfu_id] ASC),
    CONSTRAINT [FK_tab_perfis_usuarios_tab_perfis] FOREIGN KEY ([per_id]) REFERENCES [dbo].[tab_perfis] ([per_id]),
    CONSTRAINT [FK_tab_perfis_usuarios_tab_usuarios] FOREIGN KEY ([usu_id]) REFERENCES [dbo].[tab_usuarios] ([usu_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador da tabela que faz o relacionamento entre perfil e usuário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_usuarios', @level2type = N'COLUMN', @level2name = N'pfu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de perfil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_usuarios', @level2type = N'COLUMN', @level2name = N'per_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de usuário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_usuarios', @level2type = N'COLUMN', @level2name = N'usu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_usuarios', @level2type = N'COLUMN', @level2name = N'pfu_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_usuarios', @level2type = N'COLUMN', @level2name = N'pfu_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_usuarios', @level2type = N'COLUMN', @level2name = N'pfu_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_usuarios', @level2type = N'COLUMN', @level2name = N'pfu_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_usuarios', @level2type = N'COLUMN', @level2name = N'pfu_atualizado_por';

