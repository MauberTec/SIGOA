CREATE TABLE [dbo].[tab_rodovias_rodovias] (
    [rod_id]          BIGINT         NOT NULL,
    [rod_codigo]      NVARCHAR (50)  NULL,
    [rod_tipo]        NVARCHAR (50)  NULL,
    [rod_dispositivo] NVARCHAR (500) NULL,
    [rod_km_inicial]  NVARCHAR (50)  NULL,
    [rod_km_final]    NVARCHAR (50)  NULL,
    CONSTRAINT [PK_tab_rodovias_rodovias] PRIMARY KEY CLUSTERED ([rod_id] ASC)
);

