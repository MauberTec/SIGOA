CREATE TABLE [dbo].[tab_rodovias] (
    [rod_id]               BIGINT         NOT NULL,
    [rod_tipo]             NVARCHAR (50)  NULL,
    [rod_rodovia]          NVARCHAR (50)  NULL,
    [rod_kmInicial]        NVARCHAR (50)  NULL,
    [rod_kmFinal]          NVARCHAR (50)  NULL,
    [rod_extensao]         NVARCHAR (50)  NULL,
    [rod_descricaoInicial] NVARCHAR (500) NULL,
    [rod_descricaoFinal]   NVARCHAR (500) NULL,
    [rod_administracao]    NVARCHAR (500) NULL,
    [rod_conservacao]      NVARCHAR (500) NULL,
    CONSTRAINT [PK_tab_rodovias] PRIMARY KEY CLUSTERED ([rod_id] ASC)
);

