CREATE TABLE [dbo].[tab_modulos_perfis] (
    [mfl_id]               INT           NOT NULL,
    [per_id]               INT           NOT NULL,
    [mod_id]               INT           NOT NULL,
    [mfl_leitura]          BIT           CONSTRAINT [DF_MFLREAD] DEFAULT ((1)) NOT NULL,
    [mfl_escrita]          BIT           CONSTRAINT [DF_MFLWRITE] DEFAULT ((1)) NOT NULL,
    [mfl_excluir]          BIT           CONSTRAINT [DF_MFLDELETE] DEFAULT ((1)) NOT NULL,
    [mfl_inserir]          BIT           CONSTRAINT [DF_MFLINSERT] DEFAULT ((1)) NOT NULL,
    [mfl_deletado]         DATETIME      NULL,
    [mfl_data_criacao]     DATETIME      CONSTRAINT [DF__MODULOPER__CREAT__7B5B524B] DEFAULT (getdate()) NULL,
    [mfl_criado_por]       INT           NULL,
    [mfl_data_atualizacao] DATETIME      CONSTRAINT [DF__MODULOPER__UPDAT__7C4F7684] DEFAULT (getdate()) NULL,
    [mfl_atualizado_por]   INT           NULL,
    [mfl_ip]               NVARCHAR (50) NULL,
    CONSTRAINT [PK_mod_pfl_id] PRIMARY KEY CLUSTERED ([mfl_id] ASC),
    CONSTRAINT [FK_mlf_pfl] FOREIGN KEY ([per_id]) REFERENCES [dbo].[tab_perfis] ([per_id]),
    CONSTRAINT [FK_tab_modulos_perfis_tab_modulos] FOREIGN KEY ([mod_id]) REFERENCES [dbo].[tab_modulos] ([mod_id])
);


GO
CREATE TRIGGER [dbo].[TRG_AfterUpdate_modulos_perfis] on [dbo].[tab_modulos_perfis]
FOR UPDATE 
AS

-- checa se as tmp tables existem e as exclui
set nocount on;
		declare @tabela varchar(300) = 'tab_modulos_perfis';
		declare @tra_transacao_id int = 8; --  8= Ativação 9= Desativação
		declare @mod_id_log int = -1055;
				
		declare @usu_id int;
		declare @ip varchar(30);

		declare @nRows int = (select count(*) from deleted);

		if (@nRows = 1)
		begin
				if OBJECT_ID('tempdb..#tmpComparar') is not null
				DROP TABLE #tmpComparar;

				-- insere dados NEW na tabela #tmpComparar
				select 2 as nRow, mfl_id, per_id, mod_id, mfl_leitura, mfl_escrita, mfl_excluir, mfl_inserir, mfl_deletado, mfl_data_criacao, mfl_criado_por, mfl_data_atualizacao, mfl_atualizado_por, mfl_ip  
				into #tmpComparar
				from INSERTED;

				-- insere dados OLD na tabela #tmpComparar
				insert into #tmpComparar
				select 1 as nRow, mfl_id, per_id, mod_id, mfl_leitura, mfl_escrita, mfl_excluir, mfl_inserir, mfl_deletado, mfl_data_criacao, mfl_criado_por, mfl_data_atualizacao, mfl_atualizado_por, mfl_ip  
				from DELETED;

				-- VERIFICA O QUE ESTA SENDO ALTERADO
				declare @R1 bit; declare @W1 bit; declare @X1 bit; declare @I1 bit;
				declare @R2 bit; declare @W2 bit; declare @X2 bit; declare @I2 bit;

				select  @R1 = [mfl_leitura], @W1 = [mfl_escrita], @X1 = [mfl_excluir], @I1 = [mfl_inserir]
				from deleted;

				select  @R2 = [mfl_leitura], @W2 = [mfl_escrita], @X2 = [mfl_excluir], @I2 = [mfl_inserir],
						@usu_id = [mfl_atualizado_por], @ip = [mfl_ip] 
				from inserted;

				if (@R1 <> @R2)
					if (@R1 = 1) -- é desativacao
						set @tra_transacao_id = 9;

				if (@W1 <> @W2)
					if (@W1 = 1) -- é desativacao
						set @tra_transacao_id = 9;

				if (@X1 <> @X2)
					if (@X1 = 1) -- é desativacao
						set @tra_transacao_id = 9;						

				if (@I1 <> @I2)
					if (@I1 = 1) -- é desativacao
						set @tra_transacao_id = 9;

				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_COMPARA_COLS_ANTES_DEPOIS  @tabela, @retorno = @log_texto output;

				declare @mod_pai_id int = (select top 1 isnull(mod_pai_id, -1) as mod_pai_id  
											from inserted mpf 
											inner join dbo.tab_modulos md on md.mod_id = mpf.mod_id); 				
				if (@mod_pai_id < 0) -- MODULO PAI
					set @log_texto = 'ALTERAÇÃO EM BLOCO, EXPANDIDA AOS FILHOS. ' + @log_texto;

				-- insere na tabela LOG
				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_id_log,	@log_texto,	@ip	

				-- exclui a temporaria
				DROP TABLE #tmpComparar;
		end;

set nocount off;



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador da tabela que relaciona módulos a perfis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de identificador de perfil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'per_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Chave estrangeira de identificador de módulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mod_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se é de leitura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_leitura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se é de escrita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_escrita';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se permite excluir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_excluir';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se permite inserir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_inserir';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deletado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_deletado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data criação', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_data_criacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem criou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_criado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data atualização', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_data_atualizacao';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quem atualizou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_atualizado_por';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ip de quem alterou', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tab_modulos_perfis', @level2type = N'COLUMN', @level2name = N'mfl_ip';

