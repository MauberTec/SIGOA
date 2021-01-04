CREATE TABLE [dbo].[tab_senhas] (
    [sen_id]               INT            IDENTITY (1, 1) NOT NULL,
    [usu_id]               INT            NOT NULL,
    [sen_senha_id]         NVARCHAR (240) NOT NULL,
    [sen_ativo]            BIT            CONSTRAINT [DF_Pwd_ativo] DEFAULT ((1)) NOT NULL,
    [sen_mudar_senha]      BIT            CONSTRAINT [DF_tab_senhas_sen_mudar_senha] DEFAULT ((0)) NOT NULL,
    [sen_data_criacao]     DATETIME       CONSTRAINT [DF_pwd_created] DEFAULT (getdate()) NOT NULL,
    [sen_criado_por]       BIGINT         NOT NULL,
    [sen_data_atualizacao] DATETIME       NULL,
    [sen_atualizado_por]   BIGINT         NULL,
    CONSTRAINT [PK_tab_senhas] PRIMARY KEY CLUSTERED ([sen_id] ASC),
    CONSTRAINT [FK_tab_senhas_tab_usuarios] FOREIGN KEY ([usu_id]) REFERENCES [dbo].[tab_usuarios] ([usu_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Senha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_senhas', @level2type = N'COLUMN', @level2name = N'sen_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de usuário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_senhas', @level2type = N'COLUMN', @level2name = N'usu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Senha atual criptografada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_senhas', @level2type = N'COLUMN', @level2name = N'sen_senha_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_senhas', @level2type = N'COLUMN', @level2name = N'sen_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mudança de senha bloqueio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_senhas', @level2type = N'COLUMN', @level2name = N'sen_mudar_senha';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_senhas', @level2type = N'COLUMN', @level2name = N'sen_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quen criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_senhas', @level2type = N'COLUMN', @level2name = N'sen_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_senhas', @level2type = N'COLUMN', @level2name = N'sen_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_senhas', @level2type = N'COLUMN', @level2name = N'sen_atualizado_por';

