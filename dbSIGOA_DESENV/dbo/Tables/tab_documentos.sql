CREATE TABLE [dbo].[tab_documentos] (
    [doc_id]               BIGINT         NOT NULL,
    [doc_codigo]           NVARCHAR (50)  NULL,
    [doc_descricao]        NVARCHAR (255) NOT NULL,
    [dcl_id]               INT            NULL,
    [tpd_id]               NVARCHAR (3)   NOT NULL,
    [doc_caminho]          VARCHAR (MAX)  NOT NULL,
    [doc_ativo]            BIT            CONSTRAINT [DF_tab_documentos_doc_ativo] DEFAULT ((1)) NULL,
    [doc_deletado]         DATETIME       NULL,
    [doc_data_criacao]     DATETIME       NULL,
    [doc_criado_por]       INT            NULL,
    [doc_data_atualizacao] DATETIME       NULL,
    [doc_atualizado_por]   INT            NULL,
    CONSTRAINT [PK_tab_documentos] PRIMARY KEY CLUSTERED ([doc_id] ASC),
    CONSTRAINT [FK_tab_documentos_tab_documento_classes] FOREIGN KEY ([dcl_id]) REFERENCES [dbo].[tab_documento_classes_projeto] ([dcl_id]),
    CONSTRAINT [FK_tab_documentos_tab_documento_tipos] FOREIGN KEY ([tpd_id]) REFERENCES [dbo].[tab_documento_tipos] ([tpd_id])
);


GO
CREATE NONCLUSTERED INDEX [idx_doc_codigo]
    ON [dbo].[tab_documentos]([doc_codigo] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_doc_descricao]
    ON [dbo].[tab_documentos]([doc_descricao] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_tpd_id]
    ON [dbo].[tab_documentos]([tpd_id] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador  do Documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'doc_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código do Documento seguindo norma DER', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'doc_codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descrição', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'doc_descricao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'chave estrangeira identificador classe de projeto do documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'dcl_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira identificador de tipo de documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'tpd_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caminho para encontrar o arquivo no servidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'doc_caminho';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'doc_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'doc_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data de criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'doc_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'doc_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data de atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'doc_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_documentos', @level2type = N'COLUMN', @level2name = N'doc_atualizado_por';

