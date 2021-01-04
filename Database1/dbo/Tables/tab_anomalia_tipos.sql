CREATE TABLE [dbo].[tab_anomalia_tipos] (
    [atp_id]               INT            NOT NULL,
    [leg_id]               INT            NULL,
    [leg_codigo]           NVARCHAR (10)  NOT NULL,
    [atp_codigo]           INT            NULL,
    [atp_descricao]        NVARCHAR (255) NOT NULL,
    [atp_ativo]            BIT            CONSTRAINT [DF__tipo__ATIVO__6B24EA82] DEFAULT ((1)) NULL,
    [atp_deletado]         DATETIME       NULL,
    [atp_data_criacao]     DATETIME       CONSTRAINT [DF__tipo__CREATED__6C190EBB] DEFAULT (getdate()) NOT NULL,
    [atp_criado_por]       INT            NOT NULL,
    [atp_data_atualizacao] DATETIME       CONSTRAINT [DF__tipo__UPDATED__6D0D32F4] DEFAULT (getdate()) NULL,
    [atp_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_atp_id] PRIMARY KEY CLUSTERED ([atp_id] ASC),
    CONSTRAINT [FK_tab_anomalia_tipos_tab_anomalia_legendas] FOREIGN KEY ([leg_id]) REFERENCES [dbo].[tab_anomalia_legendas] ([leg_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador do tipo de anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_tipos', @level2type = N'COLUMN', @level2name = N'atp_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo do tipo de anomalia seguindo o manual de inspeções', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_tipos', @level2type = N'COLUMN', @level2name = N'leg_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição do tipo de anomalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_tipos', @level2type = N'COLUMN', @level2name = N'atp_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_tipos', @level2type = N'COLUMN', @level2name = N'atp_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_tipos', @level2type = N'COLUMN', @level2name = N'atp_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_tipos', @level2type = N'COLUMN', @level2name = N'atp_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_tipos', @level2type = N'COLUMN', @level2name = N'atp_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data Atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_tipos', @level2type = N'COLUMN', @level2name = N'atp_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_anomalia_tipos', @level2type = N'COLUMN', @level2name = N'atp_atualizado_por';

