CREATE TABLE [dbo].[tab_moedas] (
    [moe_id]               CHAR (3)      NOT NULL,
    [moe_nome]             NVARCHAR (50) NOT NULL,
    [moe_ativo]            BIT           CONSTRAINT [DF_tab_moedas_moe_ativo] DEFAULT ((1)) NOT NULL,
    [moe_deletado]         DATETIME      NULL,
    [moe_data_criacao]     DATETIME      NULL,
    [moe_criado_por]       INT           NULL,
    [moe_data_atualizacao] DATETIME      NULL,
    [moe_atualizado_por]   INT           NULL,
    CONSTRAINT [PK_tab_moedas] PRIMARY KEY CLUSTERED ([moe_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de moeda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_moedas', @level2type = N'COLUMN', @level2name = N'moe_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nome da moeda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_moedas', @level2type = N'COLUMN', @level2name = N'moe_nome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_moedas', @level2type = N'COLUMN', @level2name = N'moe_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_moedas', @level2type = N'COLUMN', @level2name = N'moe_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_moedas', @level2type = N'COLUMN', @level2name = N'moe_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_moedas', @level2type = N'COLUMN', @level2name = N'moe_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_moedas', @level2type = N'COLUMN', @level2name = N'moe_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_moedas', @level2type = N'COLUMN', @level2name = N'moe_atualizado_por';

