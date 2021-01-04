CREATE TABLE [dbo].[tab_objetos] (
    [obj_id]               BIGINT         NOT NULL,
    [clo_id]               INT            NULL,
    [tip_id]               INT            NULL,
    [obj_codigo]           NVARCHAR (200) NOT NULL,
    [obj_descricao]        NVARCHAR (255) NOT NULL,
    [obj_pai]              INT            NULL,
    [obj_organizacao]      NVARCHAR (4)   NULL,
    [obj_departamento]     NVARCHAR (4)   NULL,
    [obj_status]           NVARCHAR (10)  NULL,
    [obj_arquivo_kml]      VARCHAR (MAX)  NULL,
    [obj_ativo]            BIT            CONSTRAINT [DF_tab_objetos_obj_ativo] DEFAULT ((1)) NOT NULL,
    [obj_deletado]         DATETIME       NULL,
    [obj_data_criacao]     DATETIME       CONSTRAINT [DF_tab_objetos_obj_data_criacao] DEFAULT (getdate()) NOT NULL,
    [obj_criado_por]       INT            CONSTRAINT [DF_tab_objetos_obj_criado_por] DEFAULT ((0)) NOT NULL,
    [obj_data_atualizacao] DATETIME       NULL,
    [obj_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_objetos] PRIMARY KEY CLUSTERED ([obj_id] ASC),
    CONSTRAINT [FK_tab_objetos_tab_objeto_classes] FOREIGN KEY ([clo_id]) REFERENCES [dbo].[tab_objeto_classes] ([clo_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de classe de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'clo_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cahve estrangeira identificador de tipo de objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'tip_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código do objeto seguindo padrão DER', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Objeto pai', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_pai';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Organização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_organizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_departamento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Status do Objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Arquivo KML', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_arquivo_kml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objetos', @level2type = N'COLUMN', @level2name = N'obj_atualizado_por';

