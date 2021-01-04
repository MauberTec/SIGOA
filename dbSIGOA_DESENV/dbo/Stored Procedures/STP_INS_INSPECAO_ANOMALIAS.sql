create procedure [dbo].[STP_INS_INSPECAO_ANOMALIAS] 
@obj_id bigint,
@ins_id bigint,
@ian_numero int,
@atp_id int,
@ian_sigla nvarchar(2),
@ale_id int,
@ian_quantidade int,
@ian_espacamento int,
@ian_largura int,
@ian_comprimento int,
@ian_abertura_minima int,
@ian_abertura_maxima int,
@aca_id int,
@ian_fotografia varchar(255),
@ian_croqui varchar(255),
@ian_desenho varchar(255),
@ian_observacoes nvarchar(255),
@leg_id int,
@ian_ativo bit,
@rpt_id_sugerido int,
@rpt_id_adotado int,
@ian_quantidade_adotada  real,
@ian_quantidade_sugerida real,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @ian_id int = (select isnull(max(ian_id),0) +1 from dbo.tab_inspecoes_anomalias);
				
				-- insere novo grupo
				INSERT INTO dbo.tab_inspecoes_anomalias (ian_id, obj_id, ins_id, ian_numero, atp_id, ian_sigla, ale_id, ian_quantidade, ian_espacamento, ian_largura, ian_comprimento, ian_abertura_minima, ian_abertura_maxima, aca_id, ian_fotografia, ian_croqui, ian_desenho, ian_observacoes, leg_id, ian_ativo, ian_data_criacao, ian_criado_por )
				    values (@ian_id, @obj_id, @ins_id, @ian_numero, @atp_id, @ian_sigla, @ale_id, @ian_quantidade, @ian_espacamento, @ian_largura, @ian_comprimento, @ian_abertura_minima, @ian_abertura_maxima, @aca_id, @ian_fotografia, @ian_croqui, @ian_desenho, @ian_observacoes, @leg_id, @ian_ativo, @actionDate , @usu_id);
			

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_inspecoes_anomalias';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 540; --  = Cadastro de O.S.

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_inspecoes_anomalias
				where  ian_id= @ian_id ;

				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @ian_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
