CREATE procedure [dbo].[STP_INS_OS_STATUS] 
@sos_codigo nVARCHAR(10), 
@sos_descricao nVARCHAR(255), 
@sos_ativo int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

				declare @sos_id int
				set @sos_id = (select isnull(max(sos_id),0) +1 from dbo.tab_ordem_servico_status);

				-- insere novo status
				INSERT INTO dbo.tab_ordem_servico_status(sos_id, sos_codigo, sos_descricao, sos_ativo, sos_data_criacao, sos_criado_por )
				    values (@sos_id, @sos_codigo, @sos_descricao, @sos_ativo, @actionDate, @usu_id);
			

			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_ordem_servico_status';
				declare @tra_id int = 4; -- 4= insercao
				declare @mod_id_log int = 520; -- status de OS

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados NEW na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_ordem_servico_status
				where  sos_id= @sos_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************
		COMMIT TRAN T1

		return @sos_id;
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
