CREATE TABLE [dbo].[tab_objeto_priorizacao] (
    [pri_id]                           INT      NOT NULL,
    [obj_id]                           BIGINT   NOT NULL,
    [pri_classificacao]                INT      NOT NULL,
    [pri_data_classificacao]           DATETIME NOT NULL,
    [pri_data_inspecao]                DATETIME NOT NULL,
    [pri_nota_final]                   REAL     NOT NULL,
    [pri_nota_estutura]                REAL     NOT NULL,
    [pri_nota_durabilidade]            REAL     NOT NULL,
    [pri_nota_importancia_oae_malha]   REAL     NOT NULL,
    [pri_nota_vdm]                     REAL     NOT NULL,
    [pri_nota_principal_utilizacao]    REAL     NOT NULL,
    [pri_nota_facilidade_desvio]       REAL     NOT NULL,
    [pri_nota_gabarito_vertical]       REAL     NOT NULL,
    [pri_nota_gabarito_horizontal]     REAL     NOT NULL,
    [pri_nota_largura_plataforma]      REAL     NOT NULL,
    [pri_nota_agressividade_ambiental] REAL     NOT NULL,
    [pri_nota_trem_tipo]               REAL     NOT NULL,
    [pri_nota_barreira_seguranca]      REAL     NOT NULL,
    [pri_restricao_treminhoes]         REAL     NOT NULL,
    [pri_ativo]                        BIT      NOT NULL,
    [pri_deletado]                     DATETIME NULL,
    [pri_data_criacao]                 DATETIME NOT NULL,
    [pri_criado_por]                   INT      NOT NULL,
    [pri_data_atualizacao]             DATETIME NULL,
    [pri_atualizado_por]               INT      NULL,
    CONSTRAINT [PK_tab_hierarquizacao] PRIMARY KEY CLUSTERED ([pri_id] ASC),
    CONSTRAINT [FK_tab_objeto_hierarquizacao_tab_objetos] FOREIGN KEY ([obj_id]) REFERENCES [dbo].[tab_objetos] ([obj_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de priorizaçãode obra de arte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cahve estrangeira identificador do objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'obj_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Classificacao, ordem orde a obra de arte se encontra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_classificacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data classificação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_data_classificacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'data inspeção gerdora das notas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_data_inspecao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota Final soma da nota estrutural, durabilidade e de importância da OAE na malha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_final';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota estrutural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_estutura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota durabilidade', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_durabilidade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota funcional, soma das notas funcionals da OAE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_importancia_oae_malha';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota de Volume diário médio(VDM)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_vdm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota da principal utilização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_principal_utilizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notadefacilidade de desvio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_facilidade_desvio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota deGabarito vertical', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_gabarito_vertical';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota de gabarito horizontal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_gabarito_horizontal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota de largura da plataforma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_largura_plataforma';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota de agressividade horizontal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_agressividade_ambiental';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota de trem-tipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_trem_tipo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ota de barreira de segurança', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_nota_barreira_seguranca';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota de restrição a treminhões', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_restricao_treminhoes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_ativo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dat criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_objeto_priorizacao', @level2type = N'COLUMN', @level2name = N'pri_atualizado_por';

