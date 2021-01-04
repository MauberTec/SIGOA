CREATE TABLE [dbo].[tab_grupos_usuarios] (
    [gpu_id]               INT      IDENTITY (1, 1) NOT NULL,
    [gru_id]               INT      NOT NULL,
    [usu_id]               INT      NOT NULL,
    [gpu_deletado]         DATETIME NULL,
    [gpu_data_criacao]     DATETIME CONSTRAINT [DF__GRUPOUSUA__CREAT__6E01572D] DEFAULT (getdate()) NULL,
    [gpu_criado_por]       INT      NULL,
    [gpu_data_atualizacao] DATETIME CONSTRAINT [DF__GRUPOUSUA__UPDAT__6EF57B66] DEFAULT (getdate()) NULL,
    [gpu_atualizado_por]   INT      NULL,
    CONSTRAINT [PK_GPU_id] PRIMARY KEY CLUSTERED ([gpu_id] ASC),
    CONSTRAINT [FK_GPU_GRP] FOREIGN KEY ([gru_id]) REFERENCES [dbo].[tab_grupos] ([gru_id]),
    CONSTRAINT [FK_tab_grupos_usuarios_tab_usuarios] FOREIGN KEY ([usu_id]) REFERENCES [dbo].[tab_usuarios] ([usu_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador na tabela que relaciona grupo a usuário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos_usuarios', @level2type = N'COLUMN', @level2name = N'gpu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos_usuarios', @level2type = N'COLUMN', @level2name = N'gru_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de usuário', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos_usuarios', @level2type = N'COLUMN', @level2name = N'usu_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos_usuarios', @level2type = N'COLUMN', @level2name = N'gpu_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos_usuarios', @level2type = N'COLUMN', @level2name = N'gpu_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos_usuarios', @level2type = N'COLUMN', @level2name = N'gpu_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos_usuarios', @level2type = N'COLUMN', @level2name = N'gpu_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos_usuarios', @level2type = N'COLUMN', @level2name = N'gpu_atualizado_por';

