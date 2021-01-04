CREATE procedure [dbo].[STP_INS_ANOM_STATUS] 
@ast_codigo nVARCHAR(10), 
@ast_descricao nVARCHAR(255), 
@ast_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @ast_id int
				set @ast_id = (select isnull(max(ast_id),0) +1 from dbo.tab_anomalia_status);
				
				-- insere novo 
				INSERT INTO dbo.tab_anomalia_status (ast_id, ast_codigo, ast_descricao, ast_ativo, ast_criado_por, ast_data_criacao )
				    values (@ast_id, @ast_codigo, @ast_descricao, @ast_ativo, @usu_id, @actionDate);

	

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_anomalia_status';
				declare @tra_transaleo_id int = 4; -- 4= insercao
				declare @mod_modulo_id_log int = 250; -- status

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_anomalia_status
				where  ast_id= @ast_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_transaleo_id, @usu_id, @mod_modulo_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @ast_id
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
