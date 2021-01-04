CREATE TABLE [dbo].[tab_usuarios] (
    [usu_id]               INT            NOT NULL,
    [usu_usuario]          NVARCHAR (20)  NOT NULL,
    [usu_nome]             NVARCHAR (80)  NOT NULL,
    [usu_email]            NVARCHAR (255) NULL,
    [usu_foto]             VARCHAR (MAX)  CONSTRAINT [DF_tab_usuarios_usu_foto] DEFAULT ('data:image/png;base64,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') NULL,
    [usu_ativo]            BIT            CONSTRAINT [DF__USUARIO__ATIVO__160F4887] DEFAULT ((1)) NULL,
    [usu_deletado]         DATETIME       NULL,
    [usu_data_criacao]     DATETIME       CONSTRAINT [DF__USUARIO__CREATED__17036CC0] DEFAULT (getdate()) NULL,
    [usu_criado_por]       INT            NULL,
    [usu_data_atualizacao] DATETIME       CONSTRAINT [DF__USUARIO__UPDATED__17F790F9] DEFAULT (getdate()) NULL,
    [usu_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_USR] PRIMARY KEY CLUSTERED ([usu_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificar de usuário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'usuário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_usuario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nome usuário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_nome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fotografia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_foto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_usuarios', @level2type = N'COLUMN', @level2name = N'usu_atualizado_por';

