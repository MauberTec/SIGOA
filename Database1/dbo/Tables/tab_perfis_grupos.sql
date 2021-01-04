CREATE TABLE [dbo].[tab_perfis_grupos] (
    [pfg_id]               INT      IDENTITY (1, 1) NOT NULL,
    [per_id]               INT      NOT NULL,
    [gru_id]               INT      NOT NULL,
    [pfg_deletado]         DATETIME NULL,
    [pfg_data_criacao]     DATETIME CONSTRAINT [DF__PERFILGRU__CREAT__03F0984C] DEFAULT (getdate()) NULL,
    [pfl_criado_por]       INT      NULL,
    [pfg_data_atualizacao] DATETIME CONSTRAINT [DF__PERFILGRU__UPDAT__04E4BC85] DEFAULT (getdate()) NULL,
    [pfg_atualizado_por]   INT      NULL,
    CONSTRAINT [PK_pfg_id] PRIMARY KEY CLUSTERED ([pfg_id] ASC),
    CONSTRAINT [FK_pfg_pfl] FOREIGN KEY ([per_id]) REFERENCES [dbo].[tab_perfis] ([per_id]),
    CONSTRAINT [FK_tab_perfis_grupos_tab_grupos] FOREIGN KEY ([gru_id]) REFERENCES [dbo].[tab_grupos] ([gru_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador tabela que realciona perfis a grupos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_grupos', @level2type = N'COLUMN', @level2name = N'pfg_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'érfil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_grupos', @level2type = N'COLUMN', @level2name = N'per_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_grupos', @level2type = N'COLUMN', @level2name = N'gru_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_grupos', @level2type = N'COLUMN', @level2name = N'pfg_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_grupos', @level2type = N'COLUMN', @level2name = N'pfg_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_grupos', @level2type = N'COLUMN', @level2name = N'pfl_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_grupos', @level2type = N'COLUMN', @level2name = N'pfg_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_perfis_grupos', @level2type = N'COLUMN', @level2name = N'pfg_atualizado_por';

