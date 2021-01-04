CREATE procedure [dbo].[STP_INS_ANOM_FLUXOSTATUS]
@ast_id_de int, 
@ast_id_para int, 
@fst_descricao nVARCHAR(255), 
@fst_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @fst_id int
				set @fst_id = (select isnull(max(fst_id),0) +1 from dbo.tab_anomalia_fluxos_status);

				-- insere novo status
				INSERT INTO dbo.tab_anomalia_fluxos_status(fst_id, ast_id_de, ast_id_para, fst_descricao, fst_ativo, fst_data_criacao, fst_criado_por )
				    values (@fst_id, @ast_id_de, @ast_id_para, @fst_descricao, @fst_ativo, @actionDate, @usu_id);
			

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_anomalia_fluxos_status';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 260; -- FLUXO DE status de OS

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_anomalia_fluxos_status
				where  fst_id= @fst_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @fst_id;
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
