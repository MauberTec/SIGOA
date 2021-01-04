CREATE TABLE [dbo].[tab_grupos] (
    [gru_id]               INT            NOT NULL,
    [gru_descricao]        NVARCHAR (255) NOT NULL,
    [gru_ativo]            BIT            CONSTRAINT [DF__GRUPO__ATIVO__6B24EA82] DEFAULT ((1)) NULL,
    [gru_deletado]         DATETIME       NULL,
    [gru_data_criacao]     DATETIME       CONSTRAINT [DF__GRUPO__CREATED__6C190EBB] DEFAULT (getdate()) NULL,
    [gru_criado_por]       INT            NULL,
    [gru_data_atualizacao] DATETIME       CONSTRAINT [DF__GRUPO__UPDATED__6D0D32F4] DEFAULT (getdate()) NULL,
    [gru_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_grp_id] PRIMARY KEY CLUSTERED ([gru_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico identificador do grupo de usuários', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos', @level2type = N'COLUMN', @level2name = N'gru_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descreve o grupo de usuários', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos', @level2type = N'COLUMN', @level2name = N'gru_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag  indicador se o grupo está ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos', @level2type = N'COLUMN', @level2name = N'gru_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica a data da deleção do grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos', @level2type = N'COLUMN', @level2name = N'gru_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data da criação do grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos', @level2type = N'COLUMN', @level2name = N'gru_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código id indicador do usuário criador do grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos', @level2type = N'COLUMN', @level2name = N'gru_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data da atualização do grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos', @level2type = N'COLUMN', @level2name = N'gru_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código id do usuário que atualizou o grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_grupos', @level2type = N'COLUMN', @level2name = N'gru_atualizado_por';

