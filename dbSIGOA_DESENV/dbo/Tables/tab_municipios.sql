CREATE TABLE [dbo].[tab_municipios] (
    [mun_id]        INT            IDENTITY (1, 1) NOT NULL,
    [mun_municipio] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_tab_municipios] PRIMARY KEY CLUSTERED ([mun_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Município', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_municipios', @level2type = N'COLUMN', @level2name = N'mun_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nome do município', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_municipios', @level2type = N'COLUMN', @level2name = N'mun_municipio';

